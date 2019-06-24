using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class Guard
    {
        public abstract void GetData(object obj);
    }

    public class Antivirus : Guard
    {
        public override void GetData(object obj)
        {
            Console.WriteLine("数据杀毒处理");
            Killvirus(obj);
        }

        public void Killvirus(object obj)
        {

        }
    }

    public class File : Guard
    {
        private Antivirus anti;

        public override void GetData(object obj)
        {
            if (anti == null)
            {
                anti = new Antivirus();
            }

            anti.GetData(obj);

            Console.WriteLine("继续处理数据");
        }
    }

    public class Proxy : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始代理模式...");

            Guard guard = new File();
            guard.GetData(new object());

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
