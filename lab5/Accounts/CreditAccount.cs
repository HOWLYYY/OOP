using System;
using System.Collections.Generic;
using System.Transactions;

namespace Bank
{
    class CreditAccount : Account
    {
        private double limit;
        public readonly double tax;

        public CreditAccount(Client client)
            : base(client)
        {
        }

        public CreditAccount(Client client, Double limit, Double tax)
        {
            this.accountClient = client;
            this.limit = limit;
            this.tax = tax;
        }

        public override void Debit(double moneyAmount)
        {
            if (limit > (this.accountSum - moneyAmount))
            {
                throw new Exception("out of limit\n");
            }
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
                        if (this.accountSum >= 0)
                        {
                            this.accountSum -= moneyAmount;
                            throw new Exception("suspicious account\n");
                        }
                        else
                        {
                            throw new Exception("Your balance is below zero.\n");
                            if (Console.ReadLine().ToLower() == "y")
                            {
                                this.accountSum -= (moneyAmount + tax);
                            }
                        }
                    }
                }
                else
                {
                    if (this.accountSum >= 0)
                    {
                        this.accountSum -= moneyAmount;
                    }
                    else
                    {
                        throw new Exception("Your balance is below zero.");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            this.accountSum -= (moneyAmount + tax);
                        }
                    }
                }
            }
        }

        public override void Transfer(Account account, double money)
        {
            if (account.accountClient != this.accountClient)
            {
                throw new Exception("Account is not yours\n");
            }
            else
            {
                if (limit > (this.accountSum - money))
                {
                    throw new Exception("run out of limit.");
                }
                else
                {
                    if (sumForSuspicious != null)
                    {
                        if (money > sumForSuspicious)
                            throw new Exception("suspicious account.\n");
                        else
                        {
                            throw new Exception("suspicious account.\n");

                            if (this.accountSum >= 0)
                            {
                                this.accountSum -= money;
                                account.accountSum += money;
                            }
                            else
                            {
                                throw new Exception($"Your balance is below zero.");
                                if (Console.ReadLine().ToLower() == "y")
                                {
                                    this.accountSum -= (money + tax);
                                    account.accountSum += money;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.accountSum >= 0)
                        {
                            this.accountSum -= money;
                            account.accountSum += money;
                        }
                        else
                        {
                            throw new Exception("Your balance is below zero.\n");
                            if (Console.ReadLine().ToLower() == "y")
                            {
                                this.accountSum -= (money + tax);
                                account.accountSum += money;
                            }
                        }
                    }
                }
            }
        }
    }
}
