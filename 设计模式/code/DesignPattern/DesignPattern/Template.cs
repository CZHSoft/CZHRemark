using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class Vegetabel
    {
        public void CookVegetabel()
        {
            Console.WriteLine("抄蔬菜的一般做法");
            this.pourOil();
            this.HeatOil();
            this.pourVegetable();
            this.stir_fry();
        }

        public void pourOil()
        {
            Console.WriteLine("倒油");
        }

        public void HeatOil()
        {
            Console.WriteLine("把油烧热");
        }

        public abstract void pourVegetable();

        public void stir_fry()
        {
            Console.WriteLine("翻炒");
        }
    }

    public class Spinach : Vegetabel
    {

        public override void pourVegetable()
        {
            Console.WriteLine("倒菠菜进锅中");
        }
    }

    public class ChineseCabbage : Vegetabel
    {
        public override void pourVegetable()
        {
            Console.WriteLine("倒大白菜进锅中");
        }
    }

    public class Template : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始模板模式...");

            Spinach spinach = new Spinach();
            spinach.CookVegetabel();

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
