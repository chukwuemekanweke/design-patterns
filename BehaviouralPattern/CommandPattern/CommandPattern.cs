using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehaviouralPattern.CommandPattern
{
    public class CommandPattern
    {
    }


    #region Command Pattern

    public class BankAccount
    {
        private int balance;
        private int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {balance}");
        }

        public bool Withdraw(int amount)
        {
            if(balance - amount >= overdraftLimit)
            {

                balance -= amount;
                Console.WriteLine($"Withdrew ${amount}, balnce is now {balance}");
                return true;
            }
            return false;
        }

        public override string ToString()
        {
           return $"{nameof(balance)}: {balance}";
        }
    }


    public interface ICommand
    {
        void Call();
        void Undo();
    }


    public class BankAccountCommand : ICommand
    {
        private BankAccount bankAccount;
        private Action action;
        private int amount;
        public bool IsSuccessful;

        public BankAccountCommand(BankAccount bankAccount, Action action, int amount)
        {
            this.bankAccount = bankAccount ?? throw new ArgumentNullException(nameof(bankAccount));
            this.action = action;
            this.amount = amount;
        }

        public void Call()
        {
            switch (action)
            {
                case Action.Deposit:
                    bankAccount.Deposit(amount);
                    IsSuccessful = true;
                    break;
                case Action.Withdraw:
                    IsSuccessful = bankAccount.Withdraw(amount);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {

            if (!IsSuccessful)
                return;

            switch (action)
            {
                case Action.Deposit:
                    bankAccount.Withdraw(amount);
                    break;
                case Action.Withdraw:



                    bankAccount.Deposit(amount);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }


    public enum Action
    {
        Deposit=1,
        Withdraw
    }

    #endregion
}
