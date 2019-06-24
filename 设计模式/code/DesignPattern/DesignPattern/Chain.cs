using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public class PurchaseRequest
    {

        public double Amount
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public PurchaseRequest(double amount, string productName)
        {
            Amount = amount;
            ProductName = productName;
        }
    }

    public abstract class Approver
    {
        public Approver NextApprover
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        protected Approver(string name)
        {
            this.Name = name;
        }

        public abstract void ProcessRequest(PurchaseRequest request);
    }

    public class Manager : Approver
    {
        public Manager(string name) : base(name) { }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 10000.0)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }

            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    public class VicePresident : Approver
    {
        public VicePresident(string name) : base(name) { }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 25000.0)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }

            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    public class President : Approver
    {
        public President(string name) : base(name) { }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 100000.0)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }

            else
            {
                Console.WriteLine("Request需要组织一个会议讨论");
            }
        }
    }

    public class Chain : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始责任链模式...");

            var requestTelphone = new PurchaseRequest(4000.0, "Telphone");
            var requestSoftware = new PurchaseRequest(10000.0, "Visual Studio");
            var requestComputers = new PurchaseRequest(40000.0, "Computers");

            Approver manager = new Manager("LearningHard");
            Approver vp = new VicePresident("Tony");
            Approver pre = new President("BossTom");

            manager.NextApprover = vp;
            vp.NextApprover = pre;

            manager.ProcessRequest(requestTelphone);
            manager.ProcessRequest(requestSoftware);
            manager.ProcessRequest(requestComputers);

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
