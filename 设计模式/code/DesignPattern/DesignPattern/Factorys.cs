using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class Friend
    {
        public abstract void Play();
    }

    public class MissLin : Friend
    {
        public override void Play()
        {
            Console.WriteLine("大家好，我是林志玲");
        }
    }

    public class MissQiu : Friend
    {
        public override void Play()
        {
            Console.WriteLine("大家好，我是邱淑贞");
        }
    }

    public class Factory1 : Command
    {
        public static Friend GetGrilFans(string type)
        {
            Friend friend = null;

            if (type.Equals("lin"))
            {
                friend = new MissLin();
            }
            else if (type.Equals("qiu"))
            {
                friend = new MissQiu();
            }

            return friend;
        }

        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始简单工厂模式...");
            Console.WriteLine("林志玲和邱淑贞你选择谁，当然是邱淑贞...");
            Friend friend = GetGrilFans("qiu");
            friend.Play();
            Console.ReadLine();
            Console.Clear();
        }
    }

    public abstract class Gril
    {
        public abstract Friend BecomeFan();
    }

    public class MissLin2 : Gril
    {
        public override Friend BecomeFan()
        {
            return new MissLin();
        }
    }

    public class Factory2 : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始工厂模式...");
            Console.WriteLine("我要和林志玲成为朋友...");
            Gril gril = new MissLin2();
            Friend friend = gril.BecomeFan();
            friend.Play();
            Console.ReadLine();
            Console.Clear();
        }
    }

    public abstract class Doll
    {
        public abstract Skin CreateSkin();

        public abstract Sound CreateSound();
    }

    public abstract class Skin
    {
        public abstract void print();
    }

    public abstract class Sound
    {
        public abstract void voice();
    }

    public class LinSkin : Skin
    {
        public override void print()
        {
            Console.WriteLine("林志玲专属皮肤...");
        }
    }

    public class LinVoice : Sound
    {
        public override void voice()
        {
            Console.WriteLine("林志玲甜美声线...");
        }
    }

    public class MissLin3 : Doll
    {
        public override Skin CreateSkin()
        {
            return new LinSkin();
        }

        public override Sound CreateSound()
        {
            return new LinVoice();
        }
    }

    public class Factory3 : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始抽象工厂模式...");
            Console.WriteLine("林志玲版专属娃娃...");

            Doll doll = new MissLin3();
            doll.CreateSkin().print();
            doll.CreateSound().voice();

            Console.ReadLine();
            Console.Clear();
        }
    }

}
