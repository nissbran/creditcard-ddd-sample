using System.Collections.Generic;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Views
{ 
    public class AccountStatusView : AccountStateView
    {
        public AccountStatusView(AccountId id, IEnumerable<AccountDomainEvent> domainEvents) : base(id, domainEvents)
        {
        }
        
        public AccountStatus Status => State.Status;
    }
}