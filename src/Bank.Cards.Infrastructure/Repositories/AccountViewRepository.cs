using System.Linq;
using System.Threading.Tasks;
using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Account.Views;
using Bank.Cards.Domain.Model;
using Bank.Cards.Infrastructure.Persistence.EventStore;

namespace Bank.Cards.Infrastructure.Repositories
{
    public class AccountViewRepository : IAccountViewRepository
    {
        private readonly IEventStore _eventStore;

        public AccountViewRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<AccountBalanceView> GetAccountBalance(AccountId id)
        {
            var domainEvents = await _eventStore.GetEventsByStreamId(new AccountEventStreamId(id));

            if (domainEvents.Count == 0)
                return null;

            return new AccountBalanceView(id, domainEvents.Cast<AccountDomainEvent>());
        }

        public async Task<AccountStatusView> GetAccountStatus(AccountId id)
        {
            var domainEvents = await _eventStore.GetEventsByStreamId(new AccountEventStreamId(id));

            if (domainEvents.Count == 0)
                return null;

            return new AccountStatusView(id, domainEvents.Cast<AccountDomainEvent>());
        }
    }
}