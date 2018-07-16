using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Account.State;

namespace Bank.Cards.Domain.Account
{
    internal class AccountAggregate
    {
        internal long AggregateVersion { get; private set; }

        internal List<IDomainEvent> UncommittedEvents { get; } = new List<IDomainEvent>();
        
        internal AccountState State { get; } = new AccountState();

        public AccountAggregate(string id)
        {
            State.AccountId = id;
        }

        public AccountAggregate(IEnumerable<AccountDomainEvent> historicEvents)
        {
            foreach (var historicEvent in historicEvents)
            {
                State.AccountId = historicEvent.AggregateId;
                
                ApplyEvent(historicEvent);
                AggregateVersion++;
            }
        }
        
        internal void ApplyChange(AccountDomainEvent domainEvent)
        {
            ApplyEvent(domainEvent);
            domainEvent.AggregateId = State.AccountId;
            UncommittedEvents.Add(domainEvent);
            AggregateVersion++;
        }

        private void ApplyEvent(AccountDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case AccountCreatedEvent createdEvent:
                    State.AccountNumber = createdEvent.AccountNumber;
                    break;
                case AccountDebitedEvent accountDebitedEvent:
                    State.Balance -= accountDebitedEvent.Amount;
                    break;
                case AccountCreditedEvent accountCreditedEvent:
                    State.Balance += accountCreditedEvent.Amount;
                    break;
                case IssuerInformationSetEvent issuerInformationSetEvent:
                    State.IssuerId = issuerInformationSetEvent.IssuerId;
                    break;
            }
        }
    }
}