using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public interface ITaxStragety
    {
        double CalculateTax(double income);
    }

    public class PersonalTaxStrategy : ITaxStragety
    {
        public double CalculateTax(double income)
        {
            return income * 0.12;
        }
    }

    public class EnterpriseTaxStrategy : ITaxStragety
    {
        public double CalculateTax(double income)
        {
            return (income - 3500) > 0 ? (income - 3500) * 0.045 : 0.0;
        }
    }

    public class InterestOperation
    {
        private readonly ITaxStragety _mStrategy;

        public InterestOperation(ITaxStragety strategy)
        {
            this._mStrategy = strategy;
        }

        public double GetTax(double income)
        {
            return _mStrategy.CalculateTax(income);
        }
    }

    public class Stragety : Command
    {
        public override void Action()
        {
            

            Console.Clear();
            Console.WriteLine("开始策略者模式...");

            var operation = new InterestOperation(new PersonalTaxStrategy());
            Console.WriteLine("个人支付的税为：{0}", operation.GetTax(5000.00));

            operation = new InterestOperation(new EnterpriseTaxStrategy());
            Console.WriteLine("企业支付的税为：{0}", operation.GetTax(50000.00));

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
