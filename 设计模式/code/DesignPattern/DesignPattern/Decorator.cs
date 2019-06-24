using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class Tombarthite
    {
        public abstract void process();
    }

    public class Phone : Tombarthite
    {
        public override void process()
        {
            Console.WriteLine("这是用稀土制造的手机");
        }
    }

    public abstract class Product : Tombarthite
    {
        private Tombarthite tom;

        public Product(Tombarthite t)
        {
            this.tom = t;
        }

        public override void process()
        {
            if(tom!=null)
            {
                tom.process();
            }
        }
    }

    public class Chip : Product
    {
        public Chip(Tombarthite t): base(t)
        {
        }

        public override void process()
        {
            base.process();

            Show();
        }

        public void Show()
        {
            Console.WriteLine("华为最新的麒麟芯片技术");
        }

    }

    public class Decorator : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始装饰者模式...");

            Tombarthite tom = new Phone();
            Product p = new Chip(tom);
            p.process();

            Console.ReadLine();
            Console.Clear();
        }
    }


}
