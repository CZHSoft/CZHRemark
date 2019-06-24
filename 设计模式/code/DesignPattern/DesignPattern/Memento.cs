using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DesignPattern
{
    public class ContactPerson
    {
        public string Name
        {
            get;
            set;
        }

        public string MobileNum
        {
            get;
            set;
        }
    }

    public class MobileOwner
    {
        public List<ContactPerson> ContactPersons
        {
            get;
            set;
        }

        public MobileOwner(List<ContactPerson> persons)
        {
            ContactPersons = persons;
        }

        public ContactMemento CreateMemento()
        {
            return new ContactMemento(new List<ContactPerson>(this.ContactPersons));
        }

        public void RestoreMemento(ContactMemento memento)
        {
            if (memento != null)
            {
                this.ContactPersons = memento.ContactPersonBack;
            }
        }

        public void Show()
        {
            Console.WriteLine("联系人列表中有{0}个人，他们是:", ContactPersons.Count);

            foreach (ContactPerson p in ContactPersons)
            {
                Console.WriteLine("姓名: {0} 号码为: {1}", p.Name, p.MobileNum);
            }
        }
    }

    public class ContactMemento
    {
        public List<ContactPerson> ContactPersonBack
        {
            get;
            set;
        }

        public ContactMemento(List<ContactPerson> persons)
        {
            ContactPersonBack = persons;
        }
    }

    public class Caretaker
    {

        public Dictionary<string,
        ContactMemento> ContactMementoDic
        {
            get;
            set;
        }

        public Caretaker()
        {
            ContactMementoDic = new Dictionary<string,
            ContactMemento>();
        }
    }

    public class Memento : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始备忘录模式...");

            var persons = new List<ContactPerson> {
                new ContactPerson {
                    Name="Learning Hard",
                    MobileNum="123445"
                }

                ,
                new ContactPerson {
                    Name="Tony",
                    MobileNum="234565"
                }

                ,
                new ContactPerson {
                    Name="Jock",
                    MobileNum="231455"
                }
            };

            var mobileOwner = new MobileOwner(persons);
            mobileOwner.Show();

            var caretaker = new Caretaker();
            caretaker.ContactMementoDic.Add(DateTime.Now.ToString(), mobileOwner.CreateMemento());

            Console.WriteLine("----移除最后一个联系人--------");
            mobileOwner.ContactPersons.RemoveAt(2);
            mobileOwner.Show();

            Thread.Sleep(1000);
            caretaker.ContactMementoDic.Add(DateTime.Now.ToString(), mobileOwner.CreateMemento());

            Console.WriteLine("-------恢复联系人列表,请从以下列表选择恢复的日期------");
            var keyCollection = caretaker.ContactMementoDic.Keys;

            foreach (string k in keyCollection)
            {
                Console.WriteLine("Key = {0}", k);
            }

            while (true)
            {
                Console.Write("请输入数字,按符号退出:");

                int index = -1;

                try
                {
                    index = Int32.Parse(Console.ReadLine());
                }

                catch
                {
                    Console.WriteLine("输入的格式错误");
                    break;
                }

                ContactMemento contactMentor = null;

                if (index < keyCollection.Count && caretaker.ContactMementoDic.TryGetValue(keyCollection.ElementAt(index), out contactMentor))
                {
                    mobileOwner.RestoreMemento(contactMentor);
                    mobileOwner.Show();
                }

                else
                {
                    Console.WriteLine("输入的索引大于集合长度！");
                }
            }

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
