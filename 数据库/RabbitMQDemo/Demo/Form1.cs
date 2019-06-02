using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int Send(string queueName,string  exchangeName,string exchangeType, string[] routingKeys, string[] mess)
        {
            IConnectionFactory conFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = "127.0.0.1",//IP地址
                Port = 5672,//端口号
                UserName = "test1",//用户账号
                Password = "test1"//用户密码
            };
            using (IConnection con = conFactory.CreateConnection())//创建连接对象
            {
                using (IModel channel = con.CreateModel())//创建连接会话对象
                {

                    //声明一个队列
                    //channel.QueueDeclare(
                    //  queue: queueName,//消息队列名称
                    //  durable: false,//是否持久化
                    //  exclusive: false,//是否该连接独有(当前的连接对象)
                    //  autoDelete: false,//是否自动被删除(没有连接资源的情况下)
                    //  arguments: null
                    //   );

                    //Exchange
                    channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);
                    foreach (string s in mess)
                    {
                        byte[] body = Encoding.UTF8.GetBytes(s);

                        foreach(var key in routingKeys)
                        {
                            channel.BasicPublish(exchange: exchangeName, routingKey: key, basicProperties: null, body: body);
                        }
                    }

                    return 1;

                }
            }
        }

        private string ReceiveQueue()
        {
            IConnectionFactory connFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = "127.0.0.1",//IP地址
                Port = 5672,//端口号
                UserName = "test1",//用户账号
                Password = "test1"//用户密码
            };
            using (IConnection conn = connFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    String queueName = String.Empty;
                    queueName = "queue1";
                    //声明一个队列
                    channel.QueueDeclare(
                      queue: queueName,//消息队列名称
                      durable: true,//是否缓存
                      exclusive: false,
                      autoDelete: false,
                      arguments: null
                       );
                    //创建消费者对象
                    var consumer = new QueueingBasicConsumer(channel);

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);


                    return message;
                }
            }
        }

        private bool receiveFlag = false;

        private void ReceiveEvent(string id, string queueName, string exchangeName, string exchangeType, string[] routingKeys, int timespan)
        {

            IConnectionFactory connFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = "127.0.0.1",//IP地址
                Port = 5672,//端口号
                UserName = "test1",//用户账号
                Password = "test1"//用户密码
            };
            using (IConnection conn = connFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //声明一个队列
                    //channel.QueueDeclare(
                    //  queue: queueName,//消息队列名称
                    //  durable: false,//是否缓存
                    //  exclusive: false,
                    //  autoDelete: false,
                    //  arguments: null
                    //   );

                    //公平调度
                    //channel.BasicQos(0, 1, false);

                    channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);
                    queueName = channel.QueueDeclare().QueueName;
                    var keys = string.Empty;

                    foreach (var key in routingKeys)
                    {
                        if(string.IsNullOrEmpty(key))
                        {
                            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");

                            break;
                        }

                        keys += key;

                        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: key);

                    }

                    //fanout
                    //channel.QueueBind(queue: queueName,
                    //          exchange: exchangeName,
                    //          routingKey: "");

                    //创建消费者对象
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        //用于测试延迟响应
                        Thread.Sleep(timespan);

                        byte[] message = ea.Body;//接收到的消息

                        string mess = Encoding.UTF8.GetString(message);

                        this.Invoke(new EventHandler(delegate {
                            this.txtLog.Text += $"{DateTime.Now}:消费者 {id} queue {queueName} exchange {exchangeName} type {exchangeType} routeKeys {keys} 内容 {mess} \r\n";
                        }));

                    };
                    //消费者开启监听
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    this.Invoke(new EventHandler(delegate {
                        this.txtLog.Text += $"{DateTime.Now}:消费者 {id} queue {queueName} exchange {exchangeName} type {exchangeType} routeKeys {keys} 启动监听 \r\n";
                    }));

                    while (receiveFlag)
                    {
                        Thread.Sleep(250);
                    }

                    this.Invoke(new EventHandler(delegate {
                        if (this.IsHandleCreated)
                        {
                            try
                            {
                                this.txtLog.Text += $"{DateTime.Now}:消费者 {id} 结束监听 \r\n";
                            }
                            catch
                            {

                            }
                            
                        }
                    }));

                    
                    
                }
            }
        }

        private void RPC_Client(string queueName,string num)
        {
            IConnectionFactory conFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = "127.0.0.1",//IP地址
                Port = 5672,//端口号
                UserName = "test1",//用户账号
                Password = "test1"//用户密码
            };
            using (IConnection con = conFactory.CreateConnection())//创建连接对象
            {
                using (IModel channel = con.CreateModel())//创建连接会话对象
                {
                    var flag = true;

                    var correlationId = Guid.NewGuid().ToString();

                    //申明需要监听的回调队列
                    var replyQueue = channel.QueueDeclare().QueueName;

                    var properties = channel.CreateBasicProperties();
                    properties.ReplyTo = replyQueue;//指定回调队列
                    properties.CorrelationId = correlationId;//指定消息唯一标识

                    var body = Encoding.UTF8.GetBytes(num);

                    //发布消息
                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);

                    this.Invoke(new EventHandler(delegate {
                        this.txtLog.Text += $"{DateTime.Now}: queue {queueName} guid{correlationId} 请求RPC调用 参数{num} \r\n";
                    }));

                    // //创建消费者用于处理消息回调（远程调用返回结果）
                    var callbackConsumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queue: replyQueue, autoAck: true, consumer: callbackConsumer);
                    callbackConsumer.Received += (model, ea) =>
                    {
                        //仅当消息回调的ID与发送的ID一致时，说明远程调用结果正确返回。
                        if (ea.BasicProperties.CorrelationId == correlationId)
                        {
                            flag = false;

                            this.Invoke(new EventHandler(delegate {
                                this.txtLog.Text += $"{DateTime.Now}: queue {queueName} RPC调用 返回{Encoding.UTF8.GetString(ea.Body)} \r\n";
                            }));


                        }
                    };

                    while (flag)
                    {
                        Thread.Sleep(250);
                    }

                }
            }
        }

        private bool rpcFlag = false;

        private void RPC_Server(string queueName)
        {
            IConnectionFactory connFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = "127.0.0.1",//IP地址
                Port = 5672,//端口号
                UserName = "test1",//用户账号
                Password = "test1"//用户密码
            };
            using (IConnection conn = connFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //定义queue
                    channel.QueueDeclare(
                        queue: queueName, 
                        durable: false,
                        exclusive: false, 
                        autoDelete: false, 
                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var message = Encoding.UTF8.GetString(ea.Body);

                        int n = int.Parse(message);

                        int result = RPC_Fun(n);

                        Thread.Sleep(10000);

                        //从请求的参数中获取请求的唯一标识，在消息回传时同样绑定
                        var properties = ea.BasicProperties;
                        var replyProerties = channel.CreateBasicProperties();
                        replyProerties.CorrelationId = properties.CorrelationId;

                        var body = Encoding.UTF8.GetBytes(result.ToString());

                        //将远程调用结果发送到客户端监听的队列上
                        channel.BasicPublish(exchange: "", routingKey: properties.ReplyTo,
                            basicProperties: replyProerties, body: body );
                        
                        //手动发回消息确认
                        channel.BasicAck(ea.DeliveryTag, false);

                        this.Invoke(new EventHandler(delegate {
                            this.txtLog.Text += $"{DateTime.Now}: queue {queueName} guid{replyProerties.CorrelationId} RPC服务返回运算结果 {result} \r\n";
                        }));

                    };

                    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

                    this.Invoke(new EventHandler(delegate {
                        this.txtLog.Text += $"{DateTime.Now}: queue {queueName} RPC服务启动 \r\n";
                    }));

                    while (rpcFlag)
                    {
                        Thread.Sleep(250);
                    }

                    this.Invoke(new EventHandler(delegate {
                        if (this.IsHandleCreated)
                        {
                            try
                            {
                                this.txtLog.Text += $"{DateTime.Now}: queue {queueName} RPC服务停止 \r\n";
                            }
                            catch
                            {

                            }

                        }
                    }));
                }
            }
        }

        private int RPC_Fun(int n)
        {
            if (n == 0 || n == 1) return n;
            return RPC_Fun(n - 1) + RPC_Fun(n - 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbProduct.Text))
            {
                return;
            }

            if(Send(
                tbQueueName.Text, 
                tbExchangeName.Text, 
                tbExchangeType.Text,
                tbKeys.Text.Split(' '),
                tbProduct.Text.Split(' ')) ==1)
            {
                txtLog.Text += $"{DateTime.Now}:生产者 queue {tbQueueName.Text} exchange {tbExchangeName.Text} type {tbExchangeType.Text} routeKeys {tbKeys.Text} 内容 {tbProduct.Text } \r\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            receiveFlag = true;

            Task.Run(() => ReceiveEvent(
                tbId.Text,
                tbQueueNameC.Text,
                tbExchangeNameC.Text, 
                tbExchangeTypeC.Text, 
                tbKeysC.Text.Split(' '), 
                100)
                );
        }

        private void button3_Click(object sender, EventArgs e)
        {
            receiveFlag = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            receiveFlag = false;

            rpcFlag = false;

            Thread.Sleep(1500);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Task.Run(() => RPC_Client("rpc1","10"));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            rpcFlag = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rpcFlag = true;

            Task.Run(() => RPC_Server("rpc1"));
        }
    }
}
