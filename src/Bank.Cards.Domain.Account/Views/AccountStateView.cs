using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Account.Projections;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Views
{
    public abstract class AccountStateView
    {
        internal readonly AccountStateProjection State;
        
        protected AccountStateView(AccountId id, IEnumerable<AccountDomainEvent> domainEvents)
        {
            State = new AccountStateProjection(id, domainEvents);
        }
    }
}