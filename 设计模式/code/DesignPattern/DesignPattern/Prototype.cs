using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class Prototype
    {
        public string Id { get; set; }
        public Prototype(string id)
        {
            this.Id = id;
        }

        public abstract Prototype Clone();
    }

    public class Type1 : Prototype
    {
        public Type1(string id) : base(id)
        { }

        public override Prototype Clone()
        {
            //浅拷贝  
            return (Prototype)this.MemberwiseClone();
            //深拷贝  
            //return new Prototype(base.Id);  
        }
    }

    public class Proto : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始原型模式...");

            Prototype type1 = new Type1("10086");
            Prototype copy1 = type1.Clone();
            Prototype copy2 = type1.Clone();

            Console.WriteLine($"{type1.Id}:[{type1.GetHashCode()}]");
            Console.WriteLine($"{copy1.Id}:[{copy1.GetHashCode()}]");
            Console.WriteLine($"{copy2.Id}:[{copy2.GetHashCode()}]");

            Console.ReadLine();
            Console.Clear();
        }
    }
}
