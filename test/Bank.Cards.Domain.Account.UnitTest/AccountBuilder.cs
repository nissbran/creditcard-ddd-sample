using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.UnitTest
{
    public interface IAccountBuilder
    {
        List<AccountDomainEvent> DomainEvents { get; }

        Account Build();
    }
    
    public class AccountBuilder : IAccountBuilder
    {
        public List<AccountDomainEvent> DomainEvents { get; }
        
        public AccountBuilder()
        {
            DomainEvents = new List<AccountDomainEvent>
            {
                new AccountCreatedEvent(
                    Country.Sweden, 
                    Currency.SEK, 
                    new AccountNumber("41040123"))
            };
        }
        
        public Account Build()
        {
            var accountId = AccountId.NewId();
            
            DomainEvents.ForEach(domainEvent => domainEvent.AggregateId = accountId);
            
            return new Account(accountId, DomainEvents);
        }
    }
}