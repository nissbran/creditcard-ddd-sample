using System.Collections.Generic;
using Bank.Cards.Domain.Account.Enumerations;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Account.ValueTypes;

namespace Bank.Cards.Domain.Account.State
{
    internal class AccountState
    {
        internal AccountId Id { get; }
        internal AccountNumber AccountNumber { get; private set; }
        internal AccountStatus Status { get; private set; }
        internal decimal Balance { get; private set; }
        internal decimal CreditLimit { get;  private set;}
        internal long Version { get; private set; }
        
        public AccountState(AccountId id)
        {
            Id = id;
        }
        
        public AccountState(IEnumerable<AccountDomainEvent> historicEvents)
        {
            foreach (var historicEvent in historicEvents)
            {
                if (Id == AccountId.Empty)
                    Id = AccountId.Parse(historicEvent.AggregateId);
                
                ApplyEvent(historicEvent);
            }
        } 
        
        internal void ApplyEvent(AccountDomainEvent domainEvent)
        {
            Version++;
            
            switch (domainEvent)
            {
                case AccountCreatedEvent createdEvent:
                    Status = AccountStatus.Created;
                    AccountNumber = new AccountNumber(createdEvent.AccountNumber);
                    break;
                case AccountDebitedEvent accountDebitedEvent:
                    Balance -= accountDebitedEvent.Amount;
                    break;
                case AccountCreditedEvent accountCreditedEvent:
                    Balance += accountCreditedEvent.Amount;
                    break;
                case CreditLimitChangedEvent creditLimitChangedEvent:
                    CreditLimit = creditLimitChangedEvent.CreditLimit;
                    break;
            }
        }
    }
}