using System;
using System.Collections.Generic;
using Bank.Cards.Domain.Card.Enumerations;
using Bank.Cards.Domain.Card.Events;
using Bank.Cards.Domain.Card.ValueTypes;

namespace Bank.Cards.Domain.Card.State
{
    public class CardState
    {
        internal CardId Id { get; }
        internal CardStatus Status { get; set; }
        internal string NameOnCard { get; set; }
        internal Guid AccountId { get; set; }
        
        internal long Version { get; private set; }
        internal List<DomainEvent> UncommittedEvents { get; } = new List<DomainEvent>();
        
        public CardState(CardId id)
        {
            Id = id;
        }

        public CardState(IEnumerable<CreditCardDomainEvent> historicEvents)
        {
            foreach (var historicEvent in historicEvents)
            {
                if (Id == CardId.Empty)
                    Id = CardId.Parse(historicEvent.AggregateId);
                
                ApplyEvent(historicEvent);
                Version++;
            }
        }
        
        internal void ApplyChange(CreditCardDomainEvent domainEvent)
        {
            ApplyEvent(domainEvent);
            UncommittedEvents.Add(domainEvent);
            Version++;
        }

        private void ApplyEvent(CreditCardDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case CreditCardCreatedEvent _:
                    Status = CardStatus.Created;
                    break;
                case CreditCardBlockedEvent _:
                    Status = CardStatus.Blocked;
                    break;
                case CreditCardDetailsChangedEvent cardDetailsSetEvent:
                    NameOnCard = cardDetailsSetEvent.NameOnCard;
                    break;
                case CreditCardConnectedToAccountEvent cardConnectedToAccountEvent:
                    AccountId = cardConnectedToAccountEvent.AccountId;
                    break;
            }
        }
    }
}