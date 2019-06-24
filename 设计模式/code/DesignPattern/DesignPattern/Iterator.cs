using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public interface IListCollection
    {
        IIterator GetIterator();
    }

    public interface IIterator
    {
        bool MoveNext();
        Object GetCurrent();
        void Next();
        void Reset();
    }

    public class CustomList : IListCollection
    {
        int[] collection;

        public CustomList()
        {
            collection = new int[] { 2, 4, 6, 8 };
        }

        public IIterator GetIterator()
        {
            return new CustomIterator(this);
        }

        public int Length
        {
            get
            {
                return collection.Length;
            }
        }

        public int GetElement(int index)
        {
            return collection[index];
        }
    }

    public class CustomIterator : IIterator
    {

        private CustomList _list;
        private int _index;

        public CustomIterator(CustomList list)
        {
            _list = list;
            _index = 0;
        }


        public bool MoveNext()
        {
            if (_index < _list.Length)
            {
                return true;
            }
            return false;
        }

        public Object GetCurrent()
        {
            return _list.GetElement(_index);
        }

        public void Reset()
        {
            _index = 0;
        }

        public void Next()
        {
            if (_index < _list.Length)
            {
                _index++;
            }
        }
    }

    public class Iterator : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始迭代器模式...");

            IIterator iterator;
            IListCollection list = new CustomList();
            iterator = list.GetIterator();
            while (iterator.MoveNext())
            {
                int i = (int)iterator.GetCurrent();
                Console.WriteLine(i.ToString());
                iterator.Next();
            }

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
