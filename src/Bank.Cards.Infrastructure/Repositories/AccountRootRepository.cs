using System.Linq;
using System.Threading.Tasks;
using Bank.Cards.Domain.Account;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Model;
using Bank.Cards.Infrastructure.Persistence.EventStore;

namespace Bank.Cards.Infrastructure.Repositories
{
    public class AccountRootRepository : IAccountRootRepository
    {
        private readonly IEventStore _eventStore;

        public AccountRootRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Account> GetAccountById(AccountId accountId)
        {
            var domainEvents = await _eventStore.GetEventsByStreamId(new AccountEventStreamId(accountId));

            if (domainEvents.Count == 0)
                return null;

            return new Account(accountId, domainEvents.Cast<AccountDomainEvent>());
        }

        public async Task SaveAccount(Account account)
        {
            foreach (var venueUncommittedEvent in account.UncommittedEvents)
            {
                venueUncommittedEvent.AggregateId = account.Id;
            }
            
            await _eventStore.SaveEvents(new AccountEventStreamId(account.Id), account.GetExpectedVersion(), account.UncommittedEvents);
        }
    }
    
    public sealed class AccountEventStreamId : EventStreamId
    {
        private readonly string _id;

        public override string StreamName => $"Account-{_id}";

        public AccountEventStreamId(AccountId id)
        {
            _id = id.ToString();
        }
    }
}