# ef core 基础

## 安装包
Install-package Microsoft.EntityFrameworkCore  
Install-package Microsoft.EntityFrameworkCore.SqlServer  
Install-package Microsoft.EntityFrameworkCore.Tool    

## dotnet core 配置
Startup->ConfigureServices 添加  services.AddDbContext<UserContext>(options => options.UseSqlServer("连接字符串"));    

迁移初始化: Add-Migration init  

执行更新命令：Update-Database  

更新迁移: Add-Migration updatedb

# 使用的两种方式

## code first
1.添加数据库上下文DbContext。  
2.ConfigureServices中添加上下文配置信息。  
3.在Controller中获取并使用数据库上下文
private readonly MyContext _context;  
public MyController(MyContext context)  
{  
    _context = context;  
}  

## db first
1.创建数据库和表信息。
2.ConfigureServices中添加上下文配置信息。  
3.运行命令:Scaffold-DbContext "数据库连接字符串" EF组件名(Microsoft.EntityFrameworkCore.SqlServer/Pomelo.EntityFrameworkCore.MySql/等等) -OutputDir 输出文件夹名称  
例如：Scaffold-DbContext "Data Source=.;Initial Catalog=MyDB;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models  










