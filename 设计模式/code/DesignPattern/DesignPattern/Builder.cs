using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class User
    {
        public string name { get; set; }

        private bool flow;
        public bool Flow
        {   get { return flow; }
            set { flow = value;
                Console.WriteLine($"{name}执行了审批...");
            }
        }
  
        public abstract void approve();
    }

    public class Wang : User
    {
        public override void approve()
        {
            Flow = true;
        }
    }

    public class Lee : User
    {
        public override void approve()
        {
            Flow = true;
        }
    }

    public class WorkFlow
    {
        private IList<User> users = new List<User>();

        public void Add(User user)
        {
            users.Add(user);
        }

        public void Run()
        {
            Console.WriteLine("流程开始执行...");

            foreach (var u in users)
            {
                u.approve();
            }

            Console.WriteLine("流程执行完成");
        }
    }

    public class Builder : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始建造者模式...");

            User user1 = new Wang();
            user1.name = "wang";
            User user2 = new Lee();
            user2.name = "li";
            WorkFlow flow = new WorkFlow();
            flow.Add(user1);
            flow.Add(user2);
            flow.Run();

            Console.ReadLine();
            Console.Clear();
        }
    }
}
