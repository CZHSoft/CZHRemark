using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    class FlyweightFactory
    {
        public Dictionary<string, object> datas = new Dictionary<string, object>();

        public FlyweightFactory()
        {
            datas.Add("A", "A");
            datas.Add("B", "B");
            datas.Add("C", "C");
        }

        public object GetData(string key)
        {
            if (datas[key] == null)
            {
                datas[key] = key;
            }

            return datas[key];
        }
    }

    public class Flyweight : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始享元模式...");

            FlyweightFactory fly = new FlyweightFactory();
            fly.GetData("A");

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
