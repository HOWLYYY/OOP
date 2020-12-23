using System;
using System.Collections.Generic;

namespace Bank
{
    public abstract class Account
    {
        public Client accountClient;
        public double accountSum = 0;
        public double? sumForSuspicious = null;
        public static uint id = 0;
        public List<Transaction> Transactions;

        protected Account(Client client, List<Transaction> transactions)
        {
            accountClient = client;
            Transactions = transactions;
        }

        public void Add(double moneyAmount)
        {
            this.accountSum += moneyAmount;
        }

        public bool IsDebittable(double money)
        {
            return (money < accountSum);
        } 
        public abstract void Debit(double moneyAmount);
        public abstract void Transfer(Account account, double moneyAmount);

        public string CheckType()
        {
            return $"{this.accountClient}\nAccount type: {GetType().ToString().Substring(5)}";
        }
    }
}
