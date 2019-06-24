using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class Element
    {
        public abstract void Accept(IVistor vistor);
        public abstract void Print();
    }

    public class ElementA : Element
    {
        public override void Accept(IVistor vistor)
        {
            vistor.Visit(this);
        }

        public override void Print()
        {
            Console.WriteLine("我是元素A");
        }
    }

    public class ElementB : Element
    {
        public override void Accept(IVistor vistor)
        {
            vistor.Visit(this);
        }

        public override void Print()
        {
            Console.WriteLine("我是元素B");
        }
    }

    public interface IVistor
    {
        void Visit(ElementA a);
        void Visit(ElementB b);
    }

    public class ConcreteVistor : IVistor
    {

        public void Visit(ElementA a)
        {
            a.Print();
        }

        public void Visit(ElementB b)
        {
            b.Print();
        }
    }

    public class ObjectStructure
    {
        private readonly ArrayList _elements = new ArrayList();

        public ArrayList Elements
        {
            get
            {
                return _elements;
            }
        }

        public ObjectStructure()
        {
            var ran = new Random();

            for (int i = 0; i < 6; i++)
            {
                int ranNum = ran.Next(10);

                if (ranNum > 5)
                {
                    _elements.Add(new ElementA());
                }

                else
                {
                    _elements.Add(new ElementB());
                }
            }
        }
    }

    public class Visitor : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始访问者模式...");

            var objectStructure = new ObjectStructure();
            foreach (Element e in objectStructure.Elements)
            {
                e.Accept(new ConcreteVistor());
            }

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
