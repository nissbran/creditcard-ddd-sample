using System.Linq;
using System.Threading.Tasks;
using Bank.Cards.Domain.Card;
using Bank.Cards.Domain.Card.Events;
using Bank.Cards.Domain.Card.Repositories;
using Bank.Cards.Domain.Card.ValueTypes;
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

            return new CreditCard(domainEvents.Cast<CreditCardDomainEvent>());
        }

        public async Task SaveCard(CreditCard card)
        {
            var streamVersion = card.AggregateVersion - card.UncommittedEvents.Count;

            await _eventStore.SaveEvents(new CreditCardEventStreamId(card.Id), streamVersion, card.UncommittedEvents);
        }
    }
    
    public class CreditCardEventStreamId : EventStreamId
    {
        public string CardId { get; }

        public override string StreamName => $"Card-{CardId}";

        public CreditCardEventStreamId(CardId cardId)
        {
            CardId = cardId;
        }
    }
}