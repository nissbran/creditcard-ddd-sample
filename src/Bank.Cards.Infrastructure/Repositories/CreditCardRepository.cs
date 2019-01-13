using System.Linq;
using System.Threading.Tasks;
using Bank.Cards.Domain.Card;
using Bank.Cards.Domain.Card.Events;
using Bank.Cards.Domain.Card.Repositories;
using Bank.Cards.Domain.Model;
using Bank.Cards.Infrastructure.Persistence.EventStore;

namespace Bank.Cards.Infrastructure.Repositories
{
    public class CreditCardRepository : ICreditCardRootRepository
    {
        private readonly IEventStore _eventStore;

        public CreditCardRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<CreditCard> GetCardById(CardId cardId)
        {
            var domainEvents = await _eventStore.GetEventsByStreamId(new CreditCardEventStreamId(cardId));
            
            if (domainEvents.Count == 0)
                return null;

            return new CreditCard(cardId, domainEvents.Cast<CreditCardDomainEvent>());
        }

        public async Task SaveCard(CreditCard card)
        {
            foreach (var venueUncommittedEvent in card.UncommittedEvents)
            {
                venueUncommittedEvent.AggregateId = card.Id;
            }

            await _eventStore.SaveEvents(new CreditCardEventStreamId(card.Id), card.GetExpectedVersion(), card.UncommittedEvents);
        }
    }
    
    public class CreditCardEventStreamId : EventStreamId
    {
        private readonly string _id;

        public override string StreamName => $"Card-{_id}";

        public CreditCardEventStreamId(CardId cardId)
        {
            _id = cardId;
        }
    }
}