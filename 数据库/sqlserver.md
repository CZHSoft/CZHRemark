# sql基础
## 建表和约束 test表
create table dept  
( 
	dept_id int primary key,  
	dept_name nvarchar(100) not null,  
	dept_address nvarchar(100)  
)  

create table emp  
(  --不能写成{大括号  
	emp_id int constraint pk_emp_id_hahaha primary key,  
	emp_name nvarchar(20) not null,  
	emp_sex nchar(1) ,  
	dept_id int constraint fk_dept_id_heihei foreign key references  dept(dept_id),  
)  

create table ttt  
(
	i int,  
	j int  
)

create table student  
(
	stu_id int primary key,  
	stu_sal int check (stu_sal>=1000 and stu_sal<=8000),  
	stu_sex nchar(1) default ('男')  --()可以省 在数据库中字符串是必须用''括起来的  
)  
insert into student(stu_id, stu_sal) values (1, 1000);  
insert into student values (2, 6000, '女');  
insert into student values (3, 6000, '女');  
insert into student values (4, 6000, '女');  
insert into student values (3, 6000);   --error 主键重复  
insert into student values (5, 9000);   --error 超出范围  

create table student2  
(  
	stu_id int primary key,  
	stu_sal int check (stu_sal>=1000 and stu_sal<=8000),  
	stu_sex nchar(1) default ('男'),  --()可以省 在数据库中字符串是必须用''括起来的  
	stu_name nvarchar(200) unique   
)  
insert into student2 values (1, 6000, '男', '张三');  --ok  
insert into student2 values (2, 6000, '男', '张三'); --error 违反了唯一约束  
insert into student2 values (3, 6000, '男', '李四');  --ok  
insert into student2 values (null, 6000, '男', '王五'); --error 主键不能为null  出错的信息是“不能将值 NULL 插入列 'stu_id'”  
insert into student2 values (4, 6000, '男', null); --ok 说明 唯一键允许为空  
insert into student2 values (5, 6000, '男', null);  --error  stu_name 有重复键  

create table student3  
(  
	stu_id int primary key,  
	stu_sal int check (stu_sal>=1000 and stu_sal<=8000),  
	stu_sex nchar(1) default ('男'),  --()可以省 在数据库中字符串是必须用''括起来的  
	stu_name nvarchar(200) unique not null --uniqe和not null 约束可以组合是用  
)  
insert into student2 values (3, 6000, '男', null);  --error 证明了: uniqe和not null 约束可以组合使用  

create table student4  
(  
	stu_id int primary key,  
	stu_name nvarchar(50) unique not null,  
	stu_email nvarchar(50) not null unique,  
	stu_address nvarchar(50)  
)  

drop table student4; --删除表student4  

create table student5  
(  
	stu_id int primary key,  
	stu_email nvarchar(200) not null,  
	stu_name nvarchar(20) unique,  
	stu_sal int check (stu_sal>=1000 and stu_sal<=8000),  
	stu_sex nchar(1) default '男'  
)  
insert student5 values (1, 'hb.g@163.com', 'zhangsan', 5000); --error  
insert student5 (stu_id, stu_email, stu_name, stu_sal) values (2, 'hb.g@163.com', 'zhangsan', 5000);  
insert student5 (stu_id, stu_email, stu_sal) values (3, 'hb.g@163.com', 5000);  --ok  

###多对多
--班级表  
create table banji  
(  
	banji_id int primary key,  
	banji_num int not null,  
	banji_name nvarchar(100)  
)  

--教师  
create table jiaoshi  
(  
	jiaoshi_id int primary key,  
	jiaoshi_name nvarchar(200)  
)  

--第三张表 用来模拟班级和教师的关系  
create table banji_jiaoshi_mapping  
(  
	banji_id int constraint fk_banji_id foreign key references banji(banji_id),  
	jiaoshi_id int foreign key references jiaoshi(jiaoshi_id),  
	kecheng nvarchar(20),  
	constraint pk_banji_id_jiaoshi_id primary key (banji_id, jiaoshi_id, kecheng)  
)  

## 查询  scott表
select * from emp;  
	-- * 表示所有的  
	-- from emp 表示从emp表查询  

select empno, ename from emp;  

select ename, sal from emp;  

select ename, sal*12 as "年薪" from emp;  
		--as 可以省略 记住: "年薪" 不要写成'年薪' 也不要写成 年薪  

select ename, sal*12 as "年薪", sal "月薪", job from emp;  

select 888 from emp;  
	--ok  
	--输出的行数是emp表的行数  每行只有一个字段，值是888  

select 5;  --ok   
		   --不推荐  
### distinct
select deptno from emp; --14行记录 不是3行记录  

select distinct deptno from emp;  --distince deptno 会过滤掉重复的deptno  

select distinct comm from emp;  --distinct也可以过滤掉重复的null  或者说如果有多个null 只输出一个  

select distinct comm, deptno from emp;--把comm和deptno的组合进行过滤   

select deptno, distinct comm from emp;--error 逻辑上有冲突  

select 10000 from emp; --14行记录  

### between
--查找工资在1500到3000之间(包括1500和3000)的所有的员工的信息  
select * from emp where sal>=1500 and sal<=3000;  
等价于  
select * from emp where sal between 1500 and 3000;  

--查找工资在小于1500或大于3000之间的所有的员工的信息  
select * from emp where sal<1500 or sal>3000;  
等价于  
select * from emp where sal not between 1500 and 3000;  

### in
select * from emp where sal in (1500, 3000, 5000);  
等价于  
select * from emp where sal=1500 or sal=3000 or sal=5000; 
 
--把sal既不是1500 也不是3000也不是5000的记录输出   
select * from emp where sal not in (1500, 3000, 5000);  
等价于  
select * from emp where sal<>1500 and sal<>3000 and sal<>5000;  
		--数据库中不等于有两种表示：  !=    <>   推荐使用第二种  
		--对或取反是并且  对并且取反是或  
		
### top
select * from emp;  

select top 5 * from emp;  

select top 15 percent * from emp; --输出的是3个，不是2个  

--把工资在1500到3000之间(包括1500和3000)的员工中工资最高的前4个人的信息输出  
select top 4 * from emp where sal between 1500 and 3000 order by sal desc;  --desc降序 不写则默认是升序  

### null
--输出奖金非空的员工的信息  
select * from emp where comm <> null; --输出为空 error  

select * from emp where comm != null; --输出为空 error  

select * from emp where comm = null;--输出为空 error  
	--总结：null不能参与<>  !=  =运算  

--null可以参与is  not is  
select * from emp where comm is null; --输出奖金为空的员工的信息  

select * from emp where comm is not null; --输出奖金不为空的员工的信息  

--任何类型的数据都允许为null  
create table t1 (name nvarchar(20), cnt int, riqi datetime);  

insert into t1 values (null, null, null)  

select * from t1;  

--输出每个员工的姓名 年薪(包含了奖金)  comm假设是一年的奖金  
select empno, ename, sal*12+comm "年薪" from emp;  
	--本程序证明了：null不能参与任何数据运算 否则结果永远为空  



	
	
	


