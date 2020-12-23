using System;

namespace Bank
{
    class DepositAccount : Account
    {
        private DateTime dateTime;
        public readonly double percent;
        private bool moneyRecount = false;

        public DepositAccount(Client client)
            : base(client)
        { }

        public DepositAccount(Client client, DateTime dateTime, Double percent)
        {
            this.accountClient = client;
            this.dateTime = dateTime;
            this.percent = percent;
        }

        public override void Debit(double moneyAmount)
        {
            if (DateTime.Now.CompareTo(dateTime) < 0)
            {
                throw new Exception($"Please wait {(dateTime - DateTime.Now)} untill {dateTime}\n");
            }
            else
            {
                if (sumForSuspicious != null)
                {
                    if (sumForSuspicious < moneyAmount)
                    {
                        throw new Exception("suspicious account.\n");
                    }
                    else
                    {
                        throw new Exception("suspicious account.\n");
                        if (!moneyRecount)
                        {
                            this.accountSum = moneyAmount * (1 + percent / 100.00);
                            moneyRecount = true;
                        }

                        this.accountSum -= moneyAmount;
                    }
                }
                else
                {
                    if (!moneyRecount)
                    {
                        this.accountSum = moneyAmount * (1 + percent / 100.00);
                        moneyRecount = true;
                    }

                    this.accountSum -= moneyAmount;
                }
            }
        }

        public override void Transfer(Account account, double money)
        {
            if (moneyRecount)
            {
                if (account.accountClient != this.accountClient)
                    throw new Exception("Account is not yours\n");
                else
                {
                    if (accountSum < money)
                        throw new Exception("Error: No enough money\n");
                    else
                    {
                        if (sumForSuspicious != null)
                        {
                            if (sumForSuspicious < money)
                                throw new Exception("suspicious account.\n");
                            else
                            {
                                throw new Exception("suspicious account.\n");
                                this.accountSum -= money;
                                account.accountSum += money;
                            }
                        }
                        else
                        {
                            this.accountSum -= money;
                            account.accountSum += money;
                        }
                    }
                }
            }
            else
            {
                if (!moneyRecount)
                {
                    this.accountSum = accountSum * (1 + percent / 100.00);
                    moneyRecount = true;
                }

                Transfer(account, money);
            }
        }
    }
}
