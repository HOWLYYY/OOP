using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Transaction : ITransaction 
    {
        public uint _id;
        protected Account _to;
        protected Account _from;
        protected double _money;

        public Transaction(uint id, Account from, Account to, double money)
        {
            if (money <= 0) throw new Exception("Less than 0");
            _id = id;
            _to = to;
            _from = from;
            _money = money;
        }


        public void Execute()
        {
            if (_from.IsDebittable(_money))
            {
                _to.Add(_money);
                _from.Debit(_money);
            }
            else
            {
                throw new Exception("No Money, no honey");
            }
        }

        public void Undo()
        {
            _to.Debit(_money);
            _from.Add(_money);
        }
    }
}
