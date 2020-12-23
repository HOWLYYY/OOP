namespace Bank
{
    public class Decorator
    {
        public void decorate(Account account, double limit)
        {
            if (account.accountClient.passportId == null || account.accountClient.address == null)
            {
                account.sumForSuspicious = limit;
            }
        }
    }

    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request);
    }

    public abstract class AbstractHandler : IHandler
    {
        protected IHandler NextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this.NextHandler = handler;

            return handler;
        }

        public virtual object Handle(object request)
        {
            return NextHandler?.Handle(request);
        }
    }

    public class AllMoneyPayment : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request is CreditAccount || request is CurrentAccount || request is DepositAccount)
            {
                var account = request as Account;
                account.Debit(account.accountSum);

                if (NextHandler != null)
                {
                    return NextHandler.Handle(request);
                }

                return account;
            }

            return base.Handle(request);
        }
    }

    public class PercentRecount : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request is DepositAccount)
            {
                var account = request as DepositAccount;
                account.Add(account.accountSum * account.percent / 100.0);
                return NextHandler.Handle(request);
            }
            else
            {
                if (request is CurrentAccount)
                {
                    var account = request as CurrentAccount;
                    account.Add(account.accountSum * account.percent / 100.0);
                    if (NextHandler != null)
                    {
                        return NextHandler.Handle(request);
                    }

                    return account;
                }

                return base.Handle(request);
            }
        }
    }

    public class TaxPayment : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request is CreditAccount)
            {
                var account = request as CreditAccount;
                account.Debit(account.tax);

                if (NextHandler != null)
                {
                    return NextHandler.Handle(request);
                }

                return account;
            }

            return base.Handle(request);
        }
    }
}
