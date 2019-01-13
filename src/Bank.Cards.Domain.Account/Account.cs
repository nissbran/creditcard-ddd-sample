using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Account.Projections;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account
{
    public class Account : AggregateRoot<AccountId, AccountStateProjection>
    {
        public AccountId Id => State.Id;
        
        private Account(AccountStateProjection state) : base(state)
        {
        }
        
        public Account(AccountId id, Country country, Currency currency, AccountNumber accountNumber) : this(new AccountStateProjection(id))
        {
            ApplyChange(new AccountCreatedEvent(country, currency, accountNumber));
        }

        public Account(AccountId id, IEnumerable<AccountDomainEvent> historicEvents) : this(
            new AccountStateProjection(id, historicEvents))
        {
        }

        public void SetCreditLimit(decimal creditLimit)
        {
            ApplyChange(new CreditLimitChangedEvent(creditLimit));
        }

        public void Debit(Money amount, TransactionReference reference)
        {
            var expectedBalance = State.Balance - amount;
            
            if (expectedBalance < -State.CreditLimit)
                ApplyChange(new CreditLimitHitEvent(reference));
            else
                ApplyChange(new AccountDebitedEvent(amount));
        }

        public void Credit(Money amount)
        {
            ApplyChange(new AccountCreditedEvent(amount));
        }
    }
}