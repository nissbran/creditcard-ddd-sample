using System;
using System.Collections.Generic;
using Bank.Cards.Domain.Card.Events;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Card.Projections
{
    public class CreditCardStateProjection : AggregateState<CardId>
    {
        internal CardStatus Status { get; private set; }
        internal string NameOnCard { get; private set; }
        internal AccountId AccountId { get; private set; }
        
        public CreditCardStateProjection(CardId id) : base(id)
        {
        }

        public CreditCardStateProjection(CardId id, IEnumerable<CreditCardDomainEvent> historicEvents) : base(id)
        {
            foreach (var historicEvent in historicEvents)
            {
                ApplyEvent(historicEvent);
            }
        }

        public sealed override void ApplyEvent(DomainEvent domainEvent)
        {
            Version++;
            
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