using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.ValueTypes;

namespace Bank.Cards.Domain.Account
{
    public class Account : IAggregateRoot
    {
        public string Id => _aggregate.State.AccountId;

        public long AggregateVersion => _aggregate.AggregateVersion;

        public List<IDomainEvent> UncommittedEvents => _aggregate.UncommittedEvents;

        private readonly AccountAggregate _aggregate;
        
        public Account(Guid id, string accountNumber)
        {
            _aggregate = new AccountAggregate(id.ToString());
            _aggregate.ApplyChange(new AccountCreatedEvent { AccountNumber = accountNumber });
        }

        public Account(IEnumerable<IDomainEvent> historicEvents)
        {
            _aggregate = new AccountAggregate(historicEvents.Cast<AccountDomainEvent>());
        }

        public void DebitAccount(Money amount)
        {
            _aggregate.ApplyChange(new AccountDebitedEvent(amount.Value, amount.Vat));
        }
    }
}