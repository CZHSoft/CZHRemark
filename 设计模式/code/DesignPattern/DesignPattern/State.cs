using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class StateMent
    {
        public Account Account { get; set; }

        public double Balance { get; set; }

        public double Interest { get; set; }

        public double LowerLimit { get; set; }

        public double UpperLimit { get; set; }

        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
        public abstract void PayInterest();
    }

    public class RedState : StateMent
    {
        public RedState(StateMent state)
        {
            this.Balance = state.Balance;
            this.Account = state.Account;
            Interest = 0.00;
            LowerLimit = -100.00;
            UpperLimit = 0.00;
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            Console.WriteLine("没有钱可以取了！");
        }

        public override void PayInterest()
        {
        }

        private void StateChangeCheck()
        {
            if (Balance > UpperLimit)
            {
                Account.State = new SilverState(this);
            }
        }
    }

    public class SilverState : StateMent
    {
        public SilverState(StateMent state) : this(state.Balance, state.Account) { }

        public SilverState(double balance, Account account)
        {
            this.Balance = balance;
            this.Account = account;
            Interest = 0.00;
            LowerLimit = 0.00;
            UpperLimit = 1000.00;
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            Balance -= amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {
            Balance += Interest * Balance;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (Balance < LowerLimit)
            {
                Account.State = new RedState(this);
            }

            else if (Balance > UpperLimit)
            {
                Account.State = new GoldState(this);
            }
        }
    }

    public class GoldState : StateMent
    {
        public GoldState(StateMent state)
        {
            this.Balance = state.Balance;
            this.Account = state.Account;
            Interest = 0.05;
            LowerLimit = 1000.00;
            UpperLimit = 1000000.00;
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            Balance -= amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {
            Balance += Interest * Balance;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (Balance < 0.0)
            {
                Account.State = new RedState(this);
            }

            else if (Balance < LowerLimit)
            {
                Account.State = new SilverState(this);
            }
        }
    }

    public class Account
    {
        public StateMent State { get; set; }

        public string Owner { get; set; }

        public Account(string owner)
        {
            this.Owner = owner;
            this.State = new SilverState(0.0, this);
        }

        public double Balance
        {
            get
            {
                return State.Balance;
            }
        }

        public void Deposit(double amount)
        {
            State.Deposit(amount);
            Console.WriteLine("存款金额为 {0:C}——", amount);
            Console.WriteLine("账户余额为 =:{0:C}", this.Balance);
            Console.WriteLine("账户状态为: {0}", this.State.GetType().Name);
            Console.WriteLine();
        }

        public void Withdraw(double amount)
        {
            State.Withdraw(amount);
            Console.WriteLine("取款金额为 {0:C}——", amount);
            Console.WriteLine("账户余额为 =:{0:C}", this.Balance);
            Console.WriteLine("账户状态为: {0}", this.State.GetType().Name);
            Console.WriteLine();
        }

        public void PayInterest()
        {
            State.PayInterest();
            Console.WriteLine("Interest Paid --- ");
            Console.WriteLine("账户余额为 =:{0:C}", this.Balance);
            Console.WriteLine("账户状态为: {0}", this.State.GetType().Name);
            Console.WriteLine();
        }
    }

    public class State : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始状态者模式...");

            var account = new Account("Learning Hard");
            account.Deposit(1000.0);
            account.Deposit(200.0);
            account.Deposit(600.0);

            account.PayInterest();
            account.Withdraw(2000.00);
            account.Withdraw(500.00);

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
