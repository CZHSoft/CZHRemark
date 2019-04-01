## linux下基本操作
linux安装rpm -ivh rpm软件名  
linux卸载 yum -y remove  
验证安装 mysqladmin --version  
开机自启chkconfig mysql on  
检查启动项 ntsysv  
mysqladmin -u root -p root  

## 核心目录:
/var/lib/mysql  安装目录  
/usr/share/mysql 配置文件  
/usr/bin 命名目录  
/etc/init.d/mysql 启停脚本  

## 编码处理：
查看 show variables like '%char%'  
改编码 /etc/my.cnf  
client mysql   default-character-set=utf8  
mysqld  character_set_server=utf8  
mysqld  character_set_client=utf8  
mysqld  collation_server=utf8_general_ci  

## mysql分层：
> * 连接层 
> * 服务层 
> * 引擎层 
> * 存储层

## 引擎基础:
innodb：事务优先，适合高并发，行锁  
myisam：性能优先，表锁  

查看引擎 show engines  
show variables like '%storage_engine%'  
指定引擎:  
create table tb( id int(4))ENGINE=MYISAM;  

## sql的解析
sql编写过程 select distinct  from  join on where group by having order by limit  
sql解析过程 from on join where group by having select distinct order by limit  
https://www.cnblogs.com/annsshadow/p/5037667.html  

# sql的优化:主要依靠优化索引

## mysql索引：B树（小放左，右放右）
弊端：数据量少，频繁更新的字段，很少使用的字段，都不适用索引，降低增删改的性能。  
优点：提高查询效率，降低io使用率，降低cpu使用率。  

### B树特点:
3层B树可以存放上百万条数据。  
B树一般指B+树，数据全部都放在叶节点，查询N次。  

## 索引分类：
> * 主键索引：不能重复，不能为null。
> * 单值索引：单列，一个表可以多个单值索引。create index 索引名 on 表(字段)
> * 唯一索引：仅用于不能重复的数据。create unique index 索引名 on 表(字段)
> * 复合索引：多个列构成的索引。create index 索引名 on 表(字段1，字段2...字段N)

## 索引的基本操作：
或者alter table 表名 add index 索引名(字段)  
删除索引：drop index 索引名 on 表名  
查看索引：show index from 表名  

# 性能问题：
## 分析sql执行计划：explain，模拟sql优化器执行sql
> * 1.[id/table] table数据量小的表，优先查询；id相同，由上往下执行；id值不同，id越大查询优先。本质先查内层，再查外层。
> * 2.[select_type] primary:主查询，subquery:子查询，simple:简单查询(不包含子查询和union)，derived:衍生查询(form 子查询只有一个表(临时表)，或者union的前表)，union/union result:连接查询。
> * 3.[type] 性能左到右高到低 system>const>eq_ref>ref>range>index>all
> * 4.[extra] using temporary (常见于 group by);using filesort(常见于order by) 它们性能损耗比较大。

### 总结：如果(a,b,c,d)复合索引使用顺序一致，则全部使用，部分一致则部分使用，不要跨列使用。

## 优化总结：
> * 单表优化：最佳左前缀，保持索引定义和使用顺序一致，索引逐步优化，将包含的in条件放在where的条件最后面。
> * 双表优化：小表驱动大表，索引建立在经常使用的字段。
> * 三表优化：小表驱动大表，索引建立在经常使用的字段。

## 索引失效原则：
复合索引不要跨列或无序使用；  
尽量全索引匹配；  
不要在索引上进行操作，否则索引失效；  
复合索引不能使用不等于或is null，否则会失效；  
like 尽量用常量开头，%开头可能索引失效；  
尽量不要类型转换；尽量不要用or。  

## 查询一般优化经验：
主查询数据集大用in，子查询数据集大用exist。  
优化order by：调整buffer大小；避免select * ;保证字段全部升序或者降序。  

## 查找慢查询：
慢查询日志：long_query_time 10秒，默认关闭。show variables like '%slow_query_log%';  
show global status like '%slow_queries';  
通过mysqldumpslow查看慢sql。  

## 锁机制
锁机制：解决因资源共享而造成的并发问题。  
锁分类：行锁、表锁；读锁、写锁。  
show open tables;  
lock table 表名 read/write;  
unlock tables;  

## 锁总结：
> * 会话对表加锁，该会话只能对该表进行读操作，其它操作不能进行。其它会话可以读该表，写操作需要等待，其它表没有影响。
> * 会话对表加写锁，该会话可以对该表进行任何操作，但不能对其它表进行操作。其它会话对该表操作时需要等待。

show status like 'table%';  
行锁通过事务解锁。表锁通过UNLOCK 来解锁。  
如果没有索引，行锁会转变为表锁。  

## 主从同步：
主从同步：master将记录保存在二进制日志，slave通过io线程将日志拷贝到自己的relay log中，最后slave通过sql线程依据日志将数据写入到数据库。  
show master status;  
