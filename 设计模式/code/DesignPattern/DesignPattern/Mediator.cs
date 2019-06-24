using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class PayPoint
    {

        public int ID { get; set; }
        public int Money { get; set; }
        public int Point { get; set; }

        public PayPoint()
        {
            Money = 0;
            Point = 0;
        }

        public abstract void pay(int count, AbstractMediator mediator);
    }

    public abstract class AbstractMediator
    {
        public abstract void add(PayPoint payer);
        public abstract void pay(int id, int count);
    }

    public class MediatorPater : AbstractMediator
    {
        private List<PayPoint> users;

        public MediatorPater()
        {
            users = new List<PayPoint>();
        }

        public override void add(PayPoint payer)
        {
            users.Add(payer);
        }

        public override void pay(int id, int count)
        {
            var user = users.Find(o => o.ID == id);
            user.Money -= count;
            user.Point++;
        }
    }

    public class UserA : PayPoint
    {
        public override void pay(int count, AbstractMediator mediator)
        {
            mediator.pay(base.ID,count);
        }
    }

    public class Mediator : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始中介者模式...");

            PayPoint a = new UserA();
            a.ID = 1;
            a.Money = 50;
            a.Point = 0;

            PayPoint b = new UserA();
            b.ID = 2;
            b.Money = 50;
            b.Point = 0;

            AbstractMediator mediator = new MediatorPater();
            mediator.add(a);
            mediator.add(b);

            a.pay(50, mediator);
            b.pay(60, mediator);

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
