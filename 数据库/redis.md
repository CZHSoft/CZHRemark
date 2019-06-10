# 前言
Redis是一个内存中的数据结构存储系统，它可以用作数据库、缓存和消息中间件。
 
## .net core包
Install-package StackExchange.Redis  
Install-package Microsoft.Extensions.Caching.Redis  

### 使用方法
客户端方式:  
	using StackExchange.Redis;  
	
	ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("192.168.1.2:6379,password=123456");  
    IDatabase db = redis.GetDatabase();  
    string value = "abcdefg";  
    db.StringSet("test_key", value);  
	value = db.StringGet("test_key");  
	
ASP.NET Core方式:  
	services.AddDistributedRedisCache(options =>  
    {  
        options.InstanceName = "";  
        options.Configuration = "192.168.1.2:6379,password=123456";  
    });  
	
	public class ValuesController : Controller  
	{  
		private readonly IDistributedCache _distributedCache;  

		public ValuesController(IDistributedCache distributedCache)  
		{  
			_distributedCache = distributedCache;  
		}  

		// GET api/values  
		[HttpGet]  
		public async Task<string> Get()  
		{  
			// redis operate  
			var key = "test_key";  
			var valueByte = await _distributedCache.GetAsync(key);  
			if (valueByte == null)  
			{  
				await _distributedCache.SetAsync(key, Encoding.UTF8.GetBytes("world22222"), new DistributedCacheEntryOptions().SetSlidingExpiration(DateTimeOffset.Now.AddSeconds(3000)));  
				valueByte = await _distributedCache.GetAsync(key);  
			}  
			var valueString = Encoding.UTF8.GetString(valueByte);  
			return valueString;  
		}  
	}  

## Redis 的数据类型：
> * Key 
> * String 
> * List :有序字符串的集合
> * Hashes
> * Set :无序唯一字符串的集合
> * Sorted-Set :每一个字符串元素都对应一个浮点数值，该数值叫做分数。它里面的元素通常是按照分数来排序的

## Redis 持久化方式:
> * AOF :速度和可用性 
> * RDB :灾难恢复

### Redis的配置：redis.conf文件
> * RDB 快照相关: save 5 1 (5秒内1个Key被修改就会触发快照动作)
> * AOF 快照相关：appendonly yes
> * Master-Slave Replication 主从复制：master设为 bind 0.0.0.0 ，slave设为replicaof 192.168.1.2 6379(master ip port)
> * 设置密码：requirepass password ,如果主从同步 ，设置从 masterauth password
> * 使用CONFIG SET 设置上面的参数，可以立刻生效，但不保存配置。

## String
> * Set 
> * Get 
> * incr: 每次 +1 
> * incrby：每次 +设置的值 
> * decr: -1 
> * decrby  
> * mset：一次设置多个键值 
> * mget 
> * exists ：1表示true，0表示false。 
> * del ：删除 
> * expire： 设置有效期 
> * ex：参数中设置
> * ttl ：查询失效时长。返回-2表示该key不存在；返回-1表示key存在，但是没有设置expire；返回非负数表示剩余的存活时长（秒）。 

## Hash
一个Hash里面可以存多个Key-Value对作为它的field，所以它通常可以用来表示对象。
> * HSet 
> * HGet
...

## List
通过Linked List（链表）来实现的String集合，所以插入数据的速度很快。缺点就是在数据量比较大的时候，访问某个数据的时间可能会很长。
> * LPUSH， 
> * RPUSH
> * LRANGE：取索引范围数据，LRANGE key start end
> * LPOP
> * RPOP
> * BRPOP:等待数据输出
> * BLPOP

## Set
SET是无序的String集合，它里面的元素是不会重复的。
> * SADD
> * SMEMBERS：获取set里所有的元素
> * SISMEMBER：判断某个元素是否在set里
> * SINTER: 查看多个set之间的交集
> * SPOP
> * SUNIONSTORE：多个set合并到一个set
> * SCARD：可以获取set的大小
> * SRANDMEMBER: 可以随机获取set里面的元素，但是不会移除它们

## Sorted-SET
> * ZADD
> * ZRANGE: 默认按分数由低到高把Sorted Set的元素显示出来
> * ZREVRANGE：跟ZRANGE相反
> * ZRANGEBYSCORE
> * ZREMRANGEBYSCORE
> * ZRANK
> * ZREVRANK
> * ZRANGEBYLEX


  
