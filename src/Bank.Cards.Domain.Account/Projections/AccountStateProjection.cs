using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Projections
{
    public class AccountStateProjection : AggregateState<AccountId>
    {
        internal AccountNumber AccountNumber { get; private set; }
        internal Currency Currency { get; private set; }
        internal Country Country { get; private set; }
        internal AccountStatus Status { get; private set; }
        internal Money Balance { get; private set; }
        internal decimal CreditLimit { get;  private set;}
        
        public AccountStateProjection(AccountId id) : base(id)
        {
        }

        public AccountStateProjection(AccountId id, IEnumerable<AccountDomainEvent> historicEvents) : base(id)
        {
            foreach (var historicEvent in historicEvents)
            {
                ApplyEvent(historicEvent);
            }
        }
        
        public sealed override void ApplyEvent(DomainEvent domainEvent)
        {
            Version++;
            
            switch (domainEvent)
            {
                case AccountCreatedEvent accountCreated:
                    Status = AccountStatus.Created;
                    AccountNumber = new AccountNumber(accountCreated.AccountNumber);
                    Currency = accountCreated.Currency;
                    Country = accountCreated.Country;
                    Balance = Money.Create(0, accountCreated.Currency);
                    break;
                
                case AccountDebitedEvent accountDebited:
                    Balance -= accountDebited.Amount;
                    break;
                
                case AccountCreditedEvent accountCredited:
                    Balance += accountCredited.Amount;
                    break;
                
                case CreditLimitChangedEvent creditLimitChanged:
                    CreditLimit = creditLimitChanged.CreditLimit;
                    break;
            }
        }
    }
}