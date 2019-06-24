using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public class Consumer
    {

        public Subscriber Subscriber
        {
            get;
            set;
        }

        public String Symbol
        {
            get;
            set;
        }

        public string Info
        {
            get;
            set;
        }

        public void Update()
        {
            if (Subscriber != null)
            {
                Subscriber.ReceiveAndPrintData(this);
            }
        }
    }

    public class Subscriber
    {
        public string Name
        {
            get;
            set;
        }

        public Subscriber(string name)
        {
            this.Name = name;
        }

        public void ReceiveAndPrintData(Consumer consumer)
        {
            Console.WriteLine("Notified {0} of {1}'s" + " Info is: {2}", Name, consumer.Symbol, consumer.Info);
        }
    }

    public class Observer : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始观察者模式...");

            Subscriber LearningHardSub = new Subscriber("LearningHard");
            Consumer consumer = new Consumer();
            consumer.Subscriber = LearningHardSub;
            consumer.Symbol = "user";
            consumer.Info = "Have a new fun published ....";
            consumer.Update();

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
