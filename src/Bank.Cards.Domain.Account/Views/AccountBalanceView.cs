using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Views
{
    public class AccountBalanceView : AccountStatusView
    {
        public AccountBalanceView(AccountId id, IEnumerable<AccountDomainEvent> domainEvents) : base(id, domainEvents)
        {
        }

        public decimal Balance => State.Balance;
    }
}