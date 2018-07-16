using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;

namespace Bank.Cards.Domain.Account.Views
{
    public class AccountBalanceView
    {
        public decimal Balance { get; private set; }

        public AccountBalanceView(IEnumerable<AccountDomainEvent> domainEvents)
        {
            foreach (var historicEvent in domainEvents)
            {
                ApplyEvent(historicEvent);
            }
        }
        
        private void ApplyEvent(AccountDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case AccountDebitedEvent accountDebitedEvent:
                    Balance -= accountDebitedEvent.Amount;
                    break;
                case AccountCreditedEvent accountCreditedEvent:
                    Balance += accountCreditedEvent.Amount;
                    break;
            }
        }
    }
}