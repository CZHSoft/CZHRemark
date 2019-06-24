using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPattern
{
    public class Singleton : Command 
    {
        private static Singleton uniqueInstance;

        private static readonly object locker = new object();

        private Singleton() { }

        public static Singleton GetInstance()
        {
            lock (locker)
            {
                if (uniqueInstance == null)
                {
                    uniqueInstance = new Singleton();
                }
            }

            return uniqueInstance;
        }

        public void Fun()
        {
            Console.WriteLine($"{DateTime.Now}:Singleton[{Singleton.GetInstance().GetHashCode()}] fun complete..."); 
        }

        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始单例模式...");

            for(int i =0;i<10;i++)
            {
                Task.Factory.StartNew(() => { Singleton.GetInstance().Fun(); });
            }

            Console.ReadLine();
            Console.Clear();
        }
    }
}
