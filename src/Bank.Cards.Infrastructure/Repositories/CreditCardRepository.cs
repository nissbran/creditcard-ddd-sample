using System.Threading.Tasks;
using Bank.Cards.Domain.Card;
using Bank.Cards.Domain.Card.Repositories;
using Bank.Cards.Infrastructure.Persistence.EventStore;

namespace Bank.Cards.Infrastructure.Repositories
{
    public class CreditCardRepository : ICreditCardDomainRepository
    {
        private readonly IEventStore _eventStore;

        public CreditCardRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Card> GetCardByHashedPan(string hashedPan)
        {
            var domainEvents = await _eventStore.GetEventsByStreamId(new CreditCardEventStreamId(hashedPan));
            
            if (domainEvents.Count == 0)
                return null;

            return new Card(domainEvents);
        }

        public async Task SaveCard(Card card)
        {
            var streamVersion = card.AggregateVersion - card.UncommittedEvents.Count;

            await _eventStore.SaveEvents(new CreditCardEventStreamId(card.Id), streamVersion, card.UncommittedEvents);
        }
    }
    
    public class CreditCardEventStreamId : EventStreamId
    {
        public string HashedPan { get; }

        public override string StreamName => $"Card-{HashedPan}";

        public CreditCardEventStreamId(string hashedPan)
        {
            HashedPan = hashedPan;
        }
    }
}