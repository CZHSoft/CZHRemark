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

### order by
--asc是升序的意思 默认可以不写  desc是降序  
select * from emp order by sal; --默认是按照升序排序  

select * from emp order by deptno, sal; --先按照deptno升序排序，如果deptno相同，再按照sal升序排序  

select * from emp order by deptno desc, sal;  
	--先按deptno降序排序 如果deptno相同 再按照sal升序排序  
	--记住sal是升序不是降序  
	--order by a desc, b, c, d    desc只对a产生影响 不会对后面的b  c d 产生影响  

select * from emp order by deptno, sal desc  
	--先按deptno升序，再按sal降序  

### 模糊查询
select * from emp where ename like '%A%';  --ename只要含有字母A就输出  

select * from emp where ename like 'A%';  --ename只要首字母是A的就输出  

select * from emp where ename like '%A';  --ename只要尾字母是A的就输出  

select * from emp where ename like '_A%'; --ename只要第二个字母是A的就输出  

select * from emp where ename like '_[A-F]%'; --把ename中第二个字符是A或B或C或D或E或F的记录输出  

select * from emp where ename like '_[^A-F]%'; --把ename中第二个字符不是A也不是B也不是C也不是D也不是E也不是F的记录输出  
 
create table student  
(  
	name varchar(20) null ,  
	age int  
);  
insert into student values ('张三', 88);  
insert into student values ('Tom', 66);  
insert into student values ('a_b', 22);  
insert into student values ('c%d', 44);  
insert into student values ('abc_fe', 56);  
insert into student values ('haobin', 25);  
insert into student values ('HaoBin', 88);  
insert into student values ('c%', 66);  
insert into student values ('long''s', 100);  

select * from student;  

select * from student where name like '%\%%' escape '\';  --把name中包含有%的输出 
  
select * from student where name like '%\_%' escape '\';  --把name中包含有_的输出  

### 聚合函数
select lower(ename) from emp; -- 最终返回的是14行 lower()是单行函数  

select max(sal) from emp; --返回1行 max()是多行函数  

select count(*) from emp;  --返回emp表所有记录的个数  

select count(deptno) from emp;  --返回值是14 这说明deptno重复的记录也被当做有效的记录  

select count(distinct deptno) from emp; --返回值是3  统计deptno不重复的记录的个数  

select count(comm) from emp; --返回值是4 这说明comm为null的记录不会被当做有效的记录  

select max(sal) "最高工资", min(sal) "最低工资", count(*) "员工人数" from emp; --ok  

select max(sal), lower(ename) from emp;  --error

select * from emp where comm is not null;  

select ename, sal*12+comm from emp;  

select ename, sal*12+isnull(comm, 0) "年薪" from emp;  
	--isnull(comm, 0) 如果comm是null 就返回零 否则返回comm的值  

### group by
--输出每个部门的编号 和 该部门的平均工资    
select deptno, avg(sal) as "部门平均工资" from emp group by deptno;  

-- 判断下面语句是否正确  
select deptno, avg(sal) as "部门平均工资", ename from emp group by deptno; --error   

-- 判断下面语句是否正确  
select deptno, ename from emp group by deptno; --error

--总结： 使用了group by 之后 select 中只能出现分组后的整体信息，不能出现组内的详细信息

--group by a, b 的用法
select deptno,job,sal from emp group by deptno, job; --error  

select * from emp group by deptno, job; --error  

select deptno , job, avg(sal) from emp group by deptno, job; --ok  

select deptno , job, avg(sal) "平均工资", count(*) "部门人数", sum(sal) "部门的总工资", min(sal) "部门最低工资" from emp group by deptno, job order by deptno; --ok  
	
select comm, count(*) from emp group by comm;  

select max(sal) from emp; --默认把所有的信息当做一组  

### having
--输出部门平均工资大于2000的部门的部门编号 部门的平均工资  
select deptno, avg(sal) from emp group by deptno having avg(sal) > 2000;  

--判断下列sql语句是否正确  
select deptno, avg(sal) as "平均工资" from emp group by deptno having avg(sal) > 2000;  

--判断下列sql语句是否正确
select deptno, avg(sal) as "平均工资" from emp group by deptno having "平均工资" > 2000;  --error  

--判断下列sql语句是否正确  
select deptno "部门编号", avg(sal) as "平均工资" from emp group by deptno having deptno > 1;  

--判断下列sql语句是否正确  
select deptno "部门编号", avg(sal) as "平均工资" from emp group by deptno having "部门编号" > 1;  --error  

--判断下列sql语句是否正确  
select deptno, avg(sal) as "平均工资" from emp group by deptno having  deptno > 10;  

--判断下列sql语句是否正确  
select deptno, avg(sal) as "平均工资" from emp group by deptno having count(*) > 3;  

--判断下列sql语句是否正确  
select deptno, avg(sal) as "平均工资" from emp group by deptno having ename like '%A%';  --error  

--把姓名不包含A的所有的员工按部门编号分组，
--统计输出部门平均工资大于2000的部门的部门编号 部门的平均工资
select deptno, avg(sal) "平均工资" from emp where ename not like '%A%' group by deptno having avg(sal) > 2000;  

--把工资大于2000，
--统计输出部门平均工资大于3000的部门的部门编号 部门的平均工资
select deptno, avg(sal) "平均工资", count(*) "部门人数", max(sal) "部门的最高工资" from emp where sal > 2000 group by deptno having avg(sal) > 3000;  

--判断入选语句是否正确
select deptno, avg(sal) "平均工资", count(*) "部门人数", max(sal) "部门的最高工资" from emp	group by deptno having avg(sal) > 3000 where sal > 2000;  

--总计： 所有select的参数的顺序是不允许变化的，否则编译时出错 

select count(*) from emp having avg(sal) > 1000;  

select ename, sal "工资" from emp where sal > 2000;  

select ename, sal "工资" from emp where "工资"  > 2000;  --error  

select deptno, avg(sal) "平均工资", count(*) "部门人数", max(sal) "部门的最高工资" into emp_2 from emp where sal > 2000 group by deptno having avg(sal) > 3000;  --结果保存于emp_2表

select * from emp_2;  

### 连接
select "E".ename "员工姓名", "D".dname "部门名称" from emp "E" join dept "D" on "E".deptno = "D".deptno;  

--1.select ... from A, B的用法  
--emp是14行8列 dept是5行3列  
select * from emp, dept;  

--2.select ... from A, B where ... 的用法  
--输出5行 11列  
select * from emp, dept where empno = 7369;  

3. select ... from A join B on ... 的用法  
--输出11列 70行  
select * from emp "E" join dept "D" on 1=1;  

--输出2列 70行  
select "E".ename "员工姓名", "D".dname "部门名称" from emp "E" join dept "D" on 1=1;  

--判断下面语句是否正确  
select deptno from emp "E" join dept "D" on 1=1;  --error  

--判断下面语句是否正确  
select "部门表".deptno "部门编号" from emp "员工表" join dept "部门表" on 1=1;  --error  

--考虑下面语句的输出结果是什么  
--答案： 14行 11列  
select * from emp "E" join dept "D" on "E".deptno = "D".deptno;  

--考虑下面语句的输出结果是什么  
--答案：14行 2列  
select "E".ename "员工姓名", "D".dname "部门名称" from emp "E" join dept "D" on "E".deptno = "D".deptno;  

select * from emp, dept where emp.deptno = dept.deptno;  
--等价于  
select * from emp join dept on emp.deptno = dept.deptno;  

--把工资大于2000的员工的姓名和部门的名称输出 
--sql92的实现方式
select "E".ename, "D".dname from emp "E", dept "D" where "E".sal > 2000 and "E".deptno = "D".deptno;  

--sql99的实现方式	  
select "E".ename, "D".dname from emp "E" join dept "D" on "E".deptno = "D".deptno where "E".sal > 2000;  

--把工资大于2000的员工的姓名和部门的名称输出 和 工资的等级  
--sql99标准 明显的优于sql92  
select "E".ename, "D".dname, "S".grade from emp "E" join dept "D" on "E".deptno = "D".deptno join salgrade "S" on "E".sal>= "S".losal and "E".sal <= "S".hisal where "E".sal > 2000;  

--把工资大于2000的员工的姓名和部门的名称输出 和 工资的等级   
--sql92标准  
select "E".ename, "D".dname, "S".grade from emp "E", dept "D", salgrade "S" where "E".sal > 2000  and  "E".deptno = "D".deptno and ("E".sal>= "S".losal and "E".sal <= "S".hisal);  

--输出的是350行 14列 行数是乘积 列数是之和  
select * from emp "E" join dept "D" on 1=1 join salgrade "S" on 1=1;  

select "E".ename, "D".dname, "S".grade from emp "E" where "E".sal > 2000 join dept "D" on "E".deptno = "D".deptno join salgrade "S" on "E".sal>= "S".losal and "E".sal <= "S".hisal;  --error
	
### 分页
--输出工资最高的前三个员工的所有的信息  
select top 3 * from emp order by sal desc; --从输出结果来看 先执行order by 后执行top  

--工资从高到低排序，输出工资是第4-6的员工信息  
select top 3 * from emp where empno not in (select top 3 empno from emp order by sal desc) order by sal desc;  		

--工资从高到低排序，输出工资是第7-9的员工信息  
select top 3 * from emp where empno not in (select top 6 empno from emp order by sal desc) order by sal desc;  	

--工资从高到低排序，输出工资是第10-12的员工信息  
select top 3 * from emp where empno not in (select top 9 empno from emp order by sal desc) order by sal desc;  	

--工资从高到低排序，输出工资是第13-15的员工信息  
select top 3 * from emp where empno not in (select top 12 empno from emp order by sal desc) order by sal desc;  	

--总结  
--假设每页显示n条记录，当前要显示的是第m页  
--表名是A  主键是A_id  
select top n * from A where A_id not in (select top (m-1)*n A_id from emp);  

### identity
create table student2  
(  
	student_id int primary key, --必须的为student_id赋值  
	student_name nvarchar(200) not null  
)  
insert into student2 values (1, '张三');  
insert into student2 values (2, '李四');  
insert into student2 values (1, '张三'); --error  
insert into student2 values ( '张三'); --error  
insert into student2(student_name) values ('张三');--error  

create table student3  
(  
	student_id int primary key identity(100, 5), --必须的为student_id赋值  
	student_name nvarchar(200) not null  
)
insert into student3 (student_name) values ('张三');
insert into student3 values ('李四') --ok 
delete from student3 where student_name = '李四';
insert into student3 (student_name) values ('王五');

### 视图
--求出平均工资最高的部门的编号和部门的平均工资
select * from (select deptno, avg(sal) "avg_sal" from emp group by deptno) "T" where "T"."avg_sal" = (select max("E"."avg_sal") from (select deptno, avg(sal) "avg_sal"  from emp group by deptno) "E");  

create view v$_emp_1 as select deptno, avg(sal) "avg_sal" from emp group by deptno;  

select * from  v$_emp_1 where avg_sal  = (select max(avg_sal) from v$_emp_1);  

create view v$_emp2 as select empno, ename, job, mgr, comm, deptno from emp;

select * from v$_emp2	

create view v$_a as select avg(sal) from emp; --error  

create view v$_a as select avg(sal) as "avg_sal" from emp; --ok  
			
### 事务
create table bank  
(
	customerEname nvarchar(200),  
	currentMoney money  
)
insert into bank values ('张三', 1000);  
insert into bank values ('李四', 1);  

alter table bank add constraint check_currentMoney check(currentMoney>=1);  

update bank set currentMoney=currentMoney-1000 where customerEname='张三';  
update bank set currentMoney=currentMoney+1000 where customerEname='李四';  

begin transaction  
declare @errorSum int  
set @errorSum = 0  
update bank set currentMoney=currentMoney-1000 where customerEname='张三';  
set @errorSum = @errorSum + @@error  
update bank set currentMoney=currentMoney+1000 where customerEname='李四';  
set @errorSum = @errorSum + @@error  
if (@errorSum <> 0)  
  begin  
    print '转账失败'  
    rollback transaction  
  end  
else  
  begin  
    print '转账成功'  
    commit transaction  
  end  



	











	
	
	


