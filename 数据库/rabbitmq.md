# 前言
Rabbit MQ 是在Erlang下实现的一个消息队列框架平台。
 
# 配置使用
1.在开始菜单中找到RabbitMQ Command Promt，打开控制台  
2.输入 rabbitmq-plugins enable rabbitmq_management  
3.访问 http://localhost:15672  guest  guest  
4.配置允许远程访问  
更多情况下，队列服务往往不在我们本机上，我们需要远程来控制RabbitMQ，但是默认是无法通过http://server-name:15672来访问的，可以通过修改\RabbitMQ Server\rabbitmq_server-3.6.10\etc 下Rabbitmq.config来设置允许guest用户远程登录，具体修改为如下值,然后就到服务管理器中重启RabbitMQ服务。  
默认RabbitMQ会在C:\Users\Administrator\AppData\Roaming\RabbitMQ中生成一个配置文件，rabbitmq.config里面就是实际用到的配置信息，如果图方便，也可以这里直接改。  
[{rabbit, [{loopback_users, [guest]}]}].   
 
## 关于一些定义或关键字
MQ：消息队列，队列顾名思义是FIFO，全部理解就是保存消息信息数据的队列容器或者工具。    
MQ的用途：不同进程或线程之间的数据传递或通信。  
AMQP：一种消息队列的标准协议。  
Publisher：生产者，消息的发送方。  
Consumer：消费者，消息的接收方。  
Connection：网络连接。  
Queue：队列，消息的缓冲存储区。  
Channel：信道，多路复用连接中的一条独立的双向数据流通道。  
Exchange：交换器（路由器），负责消息的路由到相应队列。  
Binding：队列与交换器间的关联绑定。消费者将关注的队列绑定到指定交换器上，以便Exchange能准确分发消息到指定队列。  
Virtual Host：虚拟主机，虚拟主机提供资源的逻辑分组和分离。包含连接，交换，队列，绑定，用户权限，策略等。  
Broker：消息队列的服务器实体。  

## .net的基础用法

消费者:  
            IConnectionFactory connFactory = new ConnectionFactory//1.创建连接工厂对象  
            {  
                HostName = "127.0.0.1",//IP地址  
                Port = 5672,//端口号  
                UserName = "test1",//用户账号  
                Password = "test1"//用户密码  
            };  
            using (IConnection conn = connFactory.CreateConnection())//2.创建连接  
            {  
                using (IModel channel = conn.CreateModel())//3.创建支持AMQP的会话对象或通道  
                {  
                    String queueName = "queue1";  
					
                    //4.声明一个队列  
                    channel.QueueDeclare(  
                      queue: queueName,//消息队列名称  
                      durable: true,//是否持久化  
                      exclusive: false,//是否该连接独有(当前的连接对象)  
                      autoDelete: false,//是否自动被删除(没有连接资源的情况下)  
                      arguments: null  
                       );  
                    //5.创建消费者对象  
                    var consumer = new EventingBasicConsumer(channel);  
					
                    string mess = string.Empty;  
                    consumer.Received += (model, ea) =>  
                    {  
                        byte[] message = ea.Body;//接收到的消息  
                        mess = Encoding.UTF8.GetString(message);  
                        
                    };  
                    //6.消费者开启监听  
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);  
					
					//这里需要用一些方法堵塞，保证方法保持监听和退出  
                }  
            }  
生产者:  
			IConnectionFactory conFactory = new ConnectionFactory//1.创建连接工厂对象  
            {  
                HostName = "127.0.0.1",//IP地址  
                Port = 5672,//端口号  
                UserName = "test1",//用户账号  
                Password = "test1"//用户密码  
            };  
            using (IConnection con = conFactory.CreateConnection())//2.创建连接对象  
            {  
                using (IModel channel = con.CreateModel())//3.创建连接会话对象  
                {  
                    String queueName = "queue1";  

                    //4.声明一个队列  
                    channel.QueueDeclare(    
                      queue: queueName,//消息队列名称    
                      durable: true,//是否持久化  
                      exclusive: false,//是否该连接独有(当前的连接对象)  
                      autoDelete: false,//是否自动被删除(没有连接资源的情况下)  
                      arguments: null  
                       );  

					string mess = "CZHSoft";  
                    byte[] body = Encoding.UTF8.GetBytes(mess);  
					//5.生产者推送消息  
                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);  
                }  
            }  
			
## 模式

### worker
MQ会默认按照消费者的接入顺序分配任务到多个消费者。 

测试结果:   
2019/6/1 20:31:18:消费者 001 启动监听  
2019/6/1 20:31:19:消费者 002 启动监听   
2019/6/1 20:31:20:消费者 003 启动监听   
2019/6/1 20:31:30:生产者 内容:1 2 3 4 5 6 7 8 9 10   
2019/6/1 20:31:30:消费者 001 内容:1   
2019/6/1 20:31:30:消费者 001 内容:4   
2019/6/1 20:31:30:消费者 001 内容:7   
2019/6/1 20:31:30:消费者 001 内容:10   
2019/6/1 20:31:31:消费者 002 内容:2   
2019/6/1 20:31:32:消费者 002 内容:5   
2019/6/1 20:31:33:消费者 002 内容:8   
2019/6/1 20:31:40:消费者 003 内容:3   
2019/6/1 20:31:50:消费者 003 内容:6   
2019/6/1 20:32:00:消费者 003 内容:9   

公平调度代码:channel.BasicQos(0, 1, false);  

### Exchange
交换器类型：direct，topic，headers和fanout。  


### Publish/Subscribe(发布订阅)

#### fanout
该模式可以通过不同的交换器名称路由到对应的队列。  
生产者:  
var exchangeName = "exchange1";  
channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");  
byte[] body = Encoding.UTF8.GetBytes("CZHSoft");  
channel.BasicPublish(exchange: exchangeName, routingKey: queueName, basicProperties: null, body: body);  
消费者:  
channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");  
var queueName = channel.QueueDeclare().QueueName;  
channel.QueueBind(queue: queueName,exchange: exchangeName,routingKey: "");  
var consumer = new EventingBasicConsumer(channel);  
consumer.Received += (model, ea) =>  
{  
    byte[] message = ea.Body;  
	string mess = Encoding.UTF8.GetString(message);  
};  
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);  

测试结果:  
2019/6/2 11:06:56:消费者 001 queue amq.gen-UTvRodju7BlGbaZzBsDIBA exchange ex1 启动监听   
2019/6/2 11:07:02:消费者 002 queue amq.gen-r_lYikq4lgmNT9DeiluPBA exchange ex1 启动监听   
2019/6/2 11:07:05:消费者 003 queue amq.gen-ha5JqGHO6ajflPkGPlsOtQ exchange ex2 启动监听   
2019/6/2 11:07:24:生产者 queue  exchange ex1 内容 1 2   
2019/6/2 11:07:24:消费者 002 queue amq.gen-r_lYikq4lgmNT9DeiluPBA exchange ex1 内容 1   
2019/6/2 11:07:24:消费者 001 queue amq.gen-UTvRodju7BlGbaZzBsDIBA exchange ex1 内容 1   
2019/6/2 11:07:24:消费者 002 queue amq.gen-r_lYikq4lgmNT9DeiluPBA exchange ex1 内容 2   
2019/6/2 11:07:24:消费者 001 queue amq.gen-UTvRodju7BlGbaZzBsDIBA exchange ex1 内容 2   
2019/6/2 11:07:49:生产者 queue  exchange ex2 内容 1 2   
2019/6/2 11:07:49:消费者 003 queue amq.gen-ha5JqGHO6ajflPkGPlsOtQ exchange ex2 内容 1   
2019/6/2 11:07:49:消费者 003 queue amq.gen-ha5JqGHO6ajflPkGPlsOtQ exchange ex2 内容 2   

### Routing

#### direct
该模式可以通过不同的条件(routeKeys)路由到对应的队列。如果所有的路由条件相同，它的效果和fanout相当。  
生产者:  
channel.ExchangeDeclare(exchange: exchangeName, type: "direct");  
...  
channel.BasicPublish(exchange: exchangeName, routingKey: "key1 key2 key3", basicProperties: null, body: body);  
消费者:  
channel.ExchangeDeclare(exchange: exchangeName, type: "direct");  
var queueName = channel.QueueDeclare().QueueName;   
channel.QueueBind(queue: queueName,exchange: exchangeName,routingKey: "key1 key3");  

测试结果:    
2019/6/2 12:36:49:消费者 001 queue amq.gen-rRVvQ6XAFcUmBxFCHiVAcQ exchange route1 type direct routeKeys red 启动监听   
2019/6/2 12:36:51:消费者 002 queue amq.gen-ufCU7wJdVox25eZFyOl0Lg exchange route1 type direct routeKeys green 启动监听   
2019/6/2 12:36:52:消费者 003 queue amq.gen-BM-tmkuvTHctNJFjkJKWvw exchange route1 type direct routeKeys redgreen 启动监听   
2019/6/2 12:37:57:生产者 queue  exchange route1 type direct routeKeys red 内容 1 2   
2019/6/2 12:37:57:消费者 003 queue amq.gen-BM-tmkuvTHctNJFjkJKWvw exchange route1 type direct routeKeys redgreen 内容 1   
2019/6/2 12:37:57:消费者 001 queue amq.gen-rRVvQ6XAFcUmBxFCHiVAcQ exchange route1 type direct routeKeys red 内容 1   
2019/6/2 12:37:57:消费者 003 queue amq.gen-BM-tmkuvTHctNJFjkJKWvw exchange route1 type direct routeKeys redgreen 内容 2   
2019/6/2 12:37:57:消费者 001 queue amq.gen-rRVvQ6XAFcUmBxFCHiVAcQ exchange route1 type direct routeKeys red 内容 2   
2019/6/2 12:38:19:生产者 queue  exchange route1 type direct routeKeys green 内容 1 2   
2019/6/2 12:38:19:消费者 003 queue amq.gen-BM-tmkuvTHctNJFjkJKWvw exchange route1 type direct routeKeys redgreen 内容 1   
2019/6/2 12:38:19:消费者 002 queue amq.gen-ufCU7wJdVox25eZFyOl0Lg exchange route1 type direct routeKeys green 内容 1   
2019/6/2 12:38:19:消费者 003 queue amq.gen-BM-tmkuvTHctNJFjkJKWvw exchange route1 type direct routeKeys redgreen 内容 2   
2019/6/2 12:38:19:消费者 002 queue amq.gen-ufCU7wJdVox25eZFyOl0Lg exchange route1 type direct routeKeys green 内容 2   
2019/6/2 12:38:35:生产者 queue  exchange route1 type direct routeKeys red green 内容 1 2   
2019/6/2 12:38:35:消费者 003 queue amq.gen-BM-tmkuvTHctNJFjkJKWvw exchange route1 type direct routeKeys redgreen 内容 1   
2019/6/2 12:38:35:消费者 001 queue amq.gen-rRVvQ6XAFcUmBxFCHiVAcQ exchange route1 type direct routeKeys red 内容 1   
2019/6/2 12:38:35:消费者 002 queue amq.gen-ufCU7wJdVox25eZFyOl0Lg exchange route1 type direct routeKeys green 内容 1   
2019/6/2 12:38:35:消费者 003 queue amq.gen-BM-tmkuvTHctNJFjkJKWvw exchange route1 type direct routeKeys redgreen 内容 1   
2019/6/2 12:38:35:消费者 001 queue amq.gen-rRVvQ6XAFcUmBxFCHiVAcQ exchange route1 type direct routeKeys red 内容 2   
2019/6/2 12:38:35:消费者 002 queue amq.gen-ufCU7wJdVox25eZFyOl0Lg exchange route1 type direct routeKeys green 内容 2   
2019/6/2 12:38:35:消费者 003 queue amq.gen-BM-tmkuvTHctNJFjkJKWvw exchange route1 type direct routeKeys redgreen 内容 2   
2019/6/2 12:38:36:消费者 003 queue amq.gen-BM-tmkuvTHctNJFjkJKWvw exchange route1 type direct routeKeys redgreen 内容 2   

#### topic
该模式非常有趣，按照不同的路由规则路由到对应队列。  
规则: "*"用于匹配一个单词，比如"a","abc"等；"#"用于匹配0个或者多个单词，比如"", "abc", "abc.def"等。  

代码与direct相当，类型值为"topic",路由条件值为路由过滤规则条件，例如"czh.*.*" ,"#.czh"等。  

测试结果:  
2019/6/2 13:36:04:消费者 001 queue amq.gen-C5iVSQUf9qSK4bGqp50LUw exchange route1 type topic routeKeys czh.*.* 启动监听   
2019/6/2 13:36:18:消费者 002 queue amq.gen-yZ4qN5gKptGUp304DJjzAA exchange route1 type topic routeKeys *.*.czh 启动监听   
2019/6/2 13:37:05:消费者 003 queue amq.gen-AooSeqa-5eMWaFh6ISmwcA exchange route1 type topic routeKeys *.money.* 启动监听   
2019/6/2 13:37:35:生产者 queue  exchange route1 type topic routeKeys czh.have.mongey 内容 1   
2019/6/2 13:37:35:消费者 001 queue amq.gen-C5iVSQUf9qSK4bGqp50LUw exchange route1 type topic routeKeys czh.*.* 内容 1   
2019/6/2 13:37:57:生产者 queue  exchange route1 type topic routeKeys good.boy.czh 内容 2   
2019/6/2 13:37:57:消费者 002 queue amq.gen-yZ4qN5gKptGUp304DJjzAA exchange route1 type topic routeKeys *.*.czh 内容 2   
2019/6/2 13:38:15:生产者 queue  exchange route1 type topic routeKeys czh.vs.czh 内容 3   
2019/6/2 13:38:15:消费者 001 queue amq.gen-C5iVSQUf9qSK4bGqp50LUw exchange route1 type topic routeKeys czh.*.* 内容 3   
2019/6/2 13:38:15:消费者 002 queue amq.gen-yZ4qN5gKptGUp304DJjzAA exchange route1 type topic routeKeys *.*.czh 内容 3   
2019/6/2 13:38:43:生产者 queue  exchange route1 type topic routeKeys no.money.czh 内容 4   
2019/6/2 13:38:43:消费者 003 queue amq.gen-AooSeqa-5eMWaFh6ISmwcA exchange route1 type topic routeKeys *.money.* 内容 4   
2019/6/2 13:38:43:消费者 002 queue amq.gen-yZ4qN5gKptGUp304DJjzAA exchange route1 type topic routeKeys *.*.czh 内容 4   
2019/6/2 13:41:37:消费者 004 queue amq.gen-e7d4_UOPBFmJLu5MNLKzhQ exchange route1 type topic routeKeys what.# 启动监听   
2019/6/2 13:41:45:生产者 queue  exchange route1 type topic routeKeys what.is.that 内容 5   
2019/6/2 13:41:45:消费者 004 queue amq.gen-e7d4_UOPBFmJLu5MNLKzhQ exchange route1 type topic routeKeys what.# 内容 5   

### RPC
RPC——Remote Procedure Call，远程过程调用。  

客户端:  
					var correlationId = Guid.NewGuid().ToString();  
                    //申明需要监听的回调队列  
                    var replyQueue = channel.QueueDeclare().QueueName;  
                    var properties = channel.CreateBasicProperties();  
                    properties.ReplyTo = replyQueue;//指定回调队列  
                    properties.CorrelationId = correlationId;//指定消息唯一标识  
                    var body = Encoding.UTF8.GetBytes(num);  
                    //发布消息  
                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);  
                    // //创建消费者用于处理消息回调  
                    var callbackConsumer = new EventingBasicConsumer(channel);  
                    channel.BasicConsume(queue: replyQueue, autoAck: true, consumer: callbackConsumer);  
                    callbackConsumer.Received += (model, ea) =>  
                    {  
                        //仅当消息回调的ID与发送的ID一致时，说明远程调用结果正确返回。  
                        if (ea.BasicProperties.CorrelationId == correlationId)  
                        {  
							//返回结果...  
                        }  
                    };  
					//这里需要堵塞  

服务端:  
					//定义queue  
                    channel.QueueDeclare(queue: queueName, durable: false,exclusive: false, autoDelete: false, arguments: null);  
                    var consumer = new EventingBasicConsumer(channel);  
                    consumer.Received += (model, ea) =>  
                    {  
                        var message = Encoding.UTF8.GetString(ea.Body);  
                        int n = int.Parse(message);  
                        int result = RPC_Fun(n);//执行函数过程  
                        //从请求的参数中获取请求的唯一标识，在消息回传时同样绑定  
                        var properties = ea.BasicProperties;  
                        var replyProerties = channel.CreateBasicProperties();  
                        replyProerties.CorrelationId = properties.CorrelationId;  
                        var body = Encoding.UTF8.GetBytes(result.ToString());  
                        //将远程调用结果发送到客户端监听的队列上  
                        channel.BasicPublish(exchange: "", routingKey: properties.ReplyTo, basicProperties: replyProerties, body: body );  
                        //手动发回消息确认  
                        channel.BasicAck(ea.DeliveryTag, false);  
                    };  
                    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);  
					//这里需要堵塞  

测试结果:  
2019/6/2 17:27:11: queue rpc1 RPC服务启动   
2019/6/2 17:27:12: queue rpc1 guidfa7c5d3d-335c-4003-925a-c8d1f92f023d 请求RPC调用 参数10   
2019/6/2 17:27:22: queue rpc1 guidfa7c5d3d-335c-4003-925a-c8d1f92f023d RPC服务返回运算结果 55   
2019/6/2 17:27:22: queue rpc1 RPC调用 返回55   

# MQ的应用场景
> * 用作消息缓存，解决高并发和高可用的场景 。
> * 用作发布订阅或观察者模式下，保证通讯之间消息的消费者和生产者的可用性和一致性，例如CAP框架。










