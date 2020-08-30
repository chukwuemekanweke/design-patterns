using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.NullObjectPattern
{
    class NullObjectPattern
    {
    }


    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }


    public class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            Console.WriteLine(msg);
        }

        public void Warn(string msg)
        {
            Console.WriteLine("WARNING!!! " + msg);
        }

    }

    /// <summary>
    /// You use an empty class to represent  a null object
    /// </summary>
    public class NullLog : ILog
    {
        public void Info(string msg)
        {
        }

        public void Warn(string msg)
        {
        }
    }

    public class BankAccount
    {

        private ILog log;

        private int balance;

        public BankAccount([CanBeNull] ILog log)
        {
            this.log = log;  
        }

        public void Deposit(int amount)
        {
            balance += amount;
            log?.Info($"Deposited {amount}, balance is now {balance}");
        }

    }


}
