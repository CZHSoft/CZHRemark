# 前言
CAP用来处理分布式事务以及提供EventBus的功能，具有轻量级，高性能，易使用等特点。  

## 安装包
Install-package DotNetCore.CAP   
Install-package DotNetCore.CAP.RabbitMQ  
Install-package DotNetCore.CAP.SqlServer   

# 使用方法

## 配置
Startup->ConfigureServices中添加服务:   
			//add CAP  
            services.AddCap(x =>  
            {  
                x.UseEntityFramework<OrderContext>(); // 使用EF  

                x.UseSqlServer(connecttext); // 使用SQL Server  

                x.UseRabbitMQ(cfg =>  
                {  
                    cfg.HostName = "127.0.0.1";  
                    cfg.Port = 5672;  
                    cfg.UserName = "test1";  
                    cfg.Password = "test1";  
                }); // 使用RabbitMQ  

                x.UseDashboard(); // Dashboard  

                // Below settings is just for demo  
                x.FailedRetryCount = 2;  
                x.FailedRetryInterval = 5;  
            });  

# 案例
模仿订单系统下单成功后，推送后续消息到各个子系统，保证分布式系统间通信的一致性。  

## 代码
订单系统的消息发布者:
    public interface IOrderRepository
    {
        bool CreateOrder(Order order);
    } 
	
	public class OrderRepository : IOrderRepository
    {
        public readonly OrderContext _context; //ef 订单上下文
        public readonly ICapPublisher _capPublisher;//cap 消息发布者

        public OrderRepository(OrderContext context, ICapPublisher capPublisher)
        {
            this._context = context;
            this._capPublisher = capPublisher;
        }

        public bool CreateOrder(Order order)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var orderEntity = new Order()
                {
                    OrderTime = order.OrderTime
                };

                _context.Orders.Add(orderEntity);
                _context.SaveChanges();

                _capPublisher.Publish("czhsoft.services.order.create", orderEntity);

                trans.Commit();
            }

            return true;
        }
    }
	
其它子系统的消息订阅者：  
	public interface IOrderSubscriberService  
    {  
        void ConsumeOrderMessage(Order message);  
    }  
	
	public class OrderSubscriberService : IOrderSubscriberService, ICapSubscribe  
    {  
        public OrderSubscriberService()  
        {  
        }  

        [CapSubscribe("czhsoft.services.order.create")]  
        public void ConsumeOrderMessage(Order message)  
        {  
            Console.Out.WriteLine($"[MsgApi] Received message : order id {message.OrderId}");  
        }  
    }  
	

	
	












