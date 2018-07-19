using System.Linq;
using System.Threading.Tasks;
using Bank.Cards.Domain.Account;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Account.ValueTypes;
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

            return new Account(domainEvents.Cast<AccountDomainEvent>());
        }

        public async Task SaveAccount(Account account)
        {
            var expectedStreamVersion = account.AggregateVersion - account.UncommittedEvents.Count;

            await _eventStore.SaveEvents(new AccountEventStreamId(account.Id), expectedStreamVersion, account.UncommittedEvents);
        }
    }
    
    public sealed class AccountEventStreamId : EventStreamId
    {
        public string Id { get; }

        public override string StreamName => $"Account-{Id}";

        public AccountEventStreamId(string id)
        {
            Id = id;
        }

        public AccountEventStreamId(AccountId id) : this(id.ToString())
        {
        }
    }
}