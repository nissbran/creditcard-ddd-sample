using System;
using System.Collections.Generic;
using Bank.Cards.Domain.Card.Events;
using Bank.Cards.Domain.Card.State;

namespace Bank.Cards.Domain.Card
{
    internal class CreditCardAggregate
    {
        internal string Id { get; }
        
        internal long AggregateVersion { get; private set; }

        internal List<IDomainEvent> UncommittedEvents { get; } = new List<IDomainEvent>();
        
        private readonly CardState _state = new CardState();

        public CreditCardAggregate(string id)
        {
            Id = id;
        }

        public CreditCardAggregate(IEnumerable<CreditCardDomainEvent> historicEvents)
        {
            foreach (var historicEvent in historicEvents)
            {
                Id = historicEvent.AggregateId;
                
                ApplyEvent(historicEvent);
                AggregateVersion++;
            }
        }
        
        internal void ApplyChange(CreditCardDomainEvent domainEvent)
        {
            ApplyEvent(domainEvent);
            UncommittedEvents.Add(domainEvent);
            AggregateVersion++;
        }

        private void ApplyEvent(CreditCardDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case CreditCardCreatedEvent creditCardCreatedEvent:
                    break;
                case CreditCardDetailsSetEvent cardDetailsSetEvent:
                    _state.NameOnCard = cardDetailsSetEvent.NameOnCard;
                    break;
                case CreditCardConnectedToAccountEvent cardConnectedToAccountEvent:
                    _state.AccountId = cardConnectedToAccountEvent.AccountId;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}