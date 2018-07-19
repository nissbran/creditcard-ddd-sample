using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;

namespace Bank.Cards.Domain.Account.Views
{
    public class AccountBalanceView : AccountStatusView
    {
        public AccountBalanceView(IEnumerable<AccountDomainEvent> domainEvents) : base(domainEvents)
        {
        }

        public decimal Balance => State.Balance;
    }
}