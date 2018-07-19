using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;

namespace Bank.Cards.Domain.Account.Views
{
    public class AccountTransactionsView
    {
        public int Count { get; private set; }
        
        protected AccountTransactionsView(IEnumerable<AccountDomainEvent> domainEvents)
        {
            foreach (var accountEvent in domainEvents)
            {
                switch (accountEvent)
                {
                    case AccountDebitedEvent accountDebitedEvent:
                        Count++;
                        break;
                    case AccountCreditedEvent accountCreditedEvent:
                        Count++;
                        break;
                }
            }
        }
    }
}