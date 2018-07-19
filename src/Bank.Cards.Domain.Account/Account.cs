using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Account.State;
using Bank.Cards.Domain.Account.ValueTypes;
using Bank.Cards.Domain.ValueTypes;

namespace Bank.Cards.Domain.Account
{
    public class Account : IAggregateRoot
    {
        public AccountId Id => _state.Id;

        public long AggregateVersion => _state.Version;
        public List<DomainEvent> UncommittedEvents { get; } = new List<DomainEvent>();

        private readonly AccountState _state;
        
        public Account(AccountId id, AccountNumber accountNumber)
        {
            _state = new AccountState(id);
            
            ApplyChange(new AccountCreatedEvent(accountNumber));
        }

        public Account(IEnumerable<AccountDomainEvent> historicEvents)
        {
            _state = new AccountState(historicEvents);
        }

        public void SetCreditLimit(decimal creditLimit)
        {
            ApplyChange(new CreditLimitChangedEvent(creditLimit));
        }

        public void Debit(Money amount, TransactionReference reference)
        {
            var expectedBalance = _state.Balance - amount;
            
            if (expectedBalance < -_state.CreditLimit)
                ApplyChange(new CreditLimitHitEvent(reference));
            else
                ApplyChange(new AccountDebitedEvent(amount));
        }

        public void Credit(Money amount)
        {
            ApplyChange(new AccountCreditedEvent(amount));
        }

        private void ApplyChange(AccountDomainEvent domainEvent)
        {
            _state.ApplyEvent(domainEvent);
            domainEvent.AggregateId = Id;
            UncommittedEvents.Add(domainEvent);
        }
    }
}