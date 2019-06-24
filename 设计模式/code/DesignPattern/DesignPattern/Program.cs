using System;

namespace DesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            
            while(true)
            {
                Console.WriteLine("-----欢迎来到设计模式!-----");
                Console.WriteLine("1.单例模式");
                Console.WriteLine("2.简单工厂模式");
                Console.WriteLine("3.工厂模式");
                Console.WriteLine("4.工厂模式");
                Console.WriteLine("5.建造者模式");
                Console.WriteLine("6.原型模式");
                Console.WriteLine("7.适配器模式");
                Console.WriteLine("8.桥接模式");
                Console.WriteLine("9.装饰者模式");
                Console.WriteLine("10.组合模式");
                Console.WriteLine("11.外观模式");
                Console.WriteLine("12.享元模式");
                Console.WriteLine("13.代理模式");
                Console.WriteLine("14.模板模式");
                Console.WriteLine("15.迭代器模式");
                Console.WriteLine("16.观察者模式");
                Console.WriteLine("17.中介者模式");
                Console.WriteLine("18.状态者模式");
                Console.WriteLine("19.策略者模式");
                Console.WriteLine("20.责任链模式");
                Console.WriteLine("21.访问者模式");
                Console.WriteLine("22.备忘录模式");
                Console.WriteLine("88.退出程序");
                Console.WriteLine("-----请键入示例标号!-----");

                int num = -1;
                do
                {
                    int.TryParse(Console.ReadLine(), out num);

                    if(num>0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("输入有误，请重新输入...");
                    }
                }
                while (true);

                Command cmd = null;
                Invoker invoke = null;

                switch (num)
                {
                    case 88:
                        return;
                    case 1:
                        cmd = Singleton.GetInstance();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 2:
                        cmd = new Factory1();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 3:
                        cmd = new Factory2();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 4:
                        cmd = new Factory3();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 5:
                        cmd = new Builder();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 6:
                        cmd = new Proto();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 7:
                        cmd = new Adapter();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 8:
                        cmd = new Bridge();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 9:
                        cmd = new Decorator();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 10:
                        cmd = new Composite();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 11:
                        cmd = new Facade();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 12:
                        cmd = new Flyweight();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 13:
                        cmd = new Proxy();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 14:
                        cmd = new Template();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 15:
                        cmd = new Iterator();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 16:
                        cmd = new Observer();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 17:
                        cmd = new Mediator();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 18:
                        cmd = new State();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 19:
                        cmd = new Stragety();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 20:
                        cmd = new Chain();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 21:
                        cmd = new Visitor();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    case 22:
                        cmd = new Memento();
                        invoke = new Invoker(cmd);
                        invoke.ExecuteCommand();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("没有该命令");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
