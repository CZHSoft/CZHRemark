using System;
using System.Data;

namespace DesignPattern
{
    public abstract class DbCore
    {
        public abstract object GetData();
        public abstract int Execute();
    }

    public class MssqlDb : DbCore
    {
        public override object GetData()
        {
            Console.WriteLine("实现mssql 查询");
            return new object();
        }
        public override int Execute()
        {
            Console.WriteLine("实现mssql 执行");
            return 1;
        }
    }

    public class MysqlDb : DbCore
    {
        public override object GetData()
        {
            Console.WriteLine("实现mysql 查询");
            return new object();
        }
        public override int Execute()
        {
            Console.WriteLine("实现mysql 执行");
            return 1;
        }
    }

    public class DbHelper
    {
        private DbCore implementor;

        public DbCore Implementor
        {
            get
            {
                return implementor;
            }

            set
            {
                implementor = value;
            }
        }

        public virtual object GetData()
        {
            return implementor.GetData();
        }

        public virtual int Execute()
        {
            return implementor.Execute();
        }
    }

    public class DBFilter : DbHelper
    {

        public override object GetData()
        {
            object dt = base.GetData();
            Console.WriteLine("这里做过滤处理");
            return dt;
        }
    }

    public class Bridge : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始桥接模式...");

            XRAdapter xr = new XRAdapter();

            DbHelper db = new DBFilter();
            db.Implementor = new MssqlDb();
            db.GetData();

            Console.ReadLine();
            Console.Clear();
        }
    }




}
