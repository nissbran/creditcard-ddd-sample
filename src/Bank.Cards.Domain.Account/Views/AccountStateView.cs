using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Account.State;

namespace Bank.Cards.Domain.Account.Views
{
    public abstract class AccountStateView
    {
        internal readonly AccountState State;
        
        protected AccountStateView(IEnumerable<AccountDomainEvent> domainEvents)
        {
            State = new AccountState(domainEvents);
        }
    }
}