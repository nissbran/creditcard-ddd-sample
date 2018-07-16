using System.Collections.Generic;
using System.Linq;
using Bank.Cards.Domain.Card.Events;

namespace Bank.Cards.Domain.Card
{
    public class Card : IAggregateRoot
    {
        public string Id => _aggregate.Id;

        public long AggregateVersion => _aggregate.AggregateVersion;

        public List<IDomainEvent> UncommittedEvents => _aggregate.UncommittedEvents;

        private readonly CreditCardAggregate _aggregate;
        
        public Card(string hashedPan)
        {
            _aggregate = new CreditCardAggregate(hashedPan);
            _aggregate.ApplyChange(new CreditCardCreatedEvent());
        }

        public Card(IEnumerable<IDomainEvent> historicEvents)
        {
            _aggregate = new CreditCardAggregate(historicEvents.Cast<CreditCardDomainEvent>());
        }
    }
}