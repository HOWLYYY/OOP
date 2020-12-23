using System;

namespace Bank
{
    class Factory
    {
        private double creditLimit;
        private double currentPercent;
        private double depositPercent;

        public Factory(double currentPercent, double depositPercent, double creditLimit)
        {
            this.currentPercent = currentPercent;
            this.depositPercent = depositPercent;
            this.creditLimit = creditLimit;
        }

        public Account CreateAccount(Client client, double? limit, DateTime? dateTime)
        {
            Decorator decorator = new Decorator();
            Account resultAccount;
            if (limit != null)
            {
                resultAccount = new CreditAccount(client, (double)limit, creditLimit);
            }
            else
            {
                if (dateTime != null)
                {
                    resultAccount = new DepositAccount(client, (DateTime)dateTime, depositPercent);
                }
                else
                {
                    resultAccount = new CurrentAccount(client, currentPercent);
                }
            }

            return resultAccount;
        }
    }
}
