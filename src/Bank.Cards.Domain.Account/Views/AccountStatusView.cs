using System.Collections.Generic;
using Bank.Cards.Domain.Account.Enumerations;
using Bank.Cards.Domain.Account.Events;

namespace Bank.Cards.Domain.Account.Views
{ 
    public class AccountStatusView : AccountStateView
    {
        public AccountStatusView(IEnumerable<AccountDomainEvent> domainEvents) : base(domainEvents)
        {
        }
        
        public AccountStatus Status => State.Status;
    }
}