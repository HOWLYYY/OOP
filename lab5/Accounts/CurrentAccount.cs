using System;
using System.Collections.Generic;

namespace Bank
{
    public class CurrentAccount : Account
    {
        public readonly double percent;

        public CurrentAccount(Client client) : base(client)
        {
        }

        public CurrentAccount(Client client, double percent) : base(client)
        {
            this.percent = percent;
        }

        public override void Debit(double moneyAmount)
        {
            if (this.accountSum < moneyAmount) throw new Exception("Error: No enough money\n");
            else
            {
                if (sumForSuspicious != null)
                {
                    if (moneyAmount > sumForSuspicious)
                    {
                        throw new Exception("suspicious account.\n");
                    }
                    else
                    {
                        this.accountSum -= moneyAmount;
                        throw new Exception("Suspicious account.\n");
                    }
                }
                else
                {
                    this.accountSum -= moneyAmount;
                }
            }
        }

        public override void Transfer(Account account, double money)
        {
            if (account.accountClient != this.accountClient) throw new Exception("Account is not yours\n");
            else
            {
                if (accountSum < money)
                    throw new Exception("Error: No enough money\n");
                else
                {
                    if (sumForSuspicious != null)
                    {
                        if (money > sumForSuspicious)
                        {
                            throw new Exception("suspicious account.\n");
                        }
                        else
                        {
                            this.accountSum -= money;
                            account.accountSum += money;
                            throw new Exception("suspicious account.\n");
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
    }
}