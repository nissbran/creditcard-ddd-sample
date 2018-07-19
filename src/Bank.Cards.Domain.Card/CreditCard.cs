using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Cards.Domain.Card.Events;
using Bank.Cards.Domain.Card.Services;
using Bank.Cards.Domain.Card.State;
using Bank.Cards.Domain.Card.ValueTypes;

namespace Bank.Cards.Domain.Card
{
    public class CreditCard : IAggregateRoot
    {
        public CardId Id => _state.Id;
        public long AggregateVersion => _state.Version;
        public List<DomainEvent> UncommittedEvents => _state.UncommittedEvents;

        private readonly CardState _state;
        
        public CreditCard(CardId cardId, Guid accountId, CardNumber cardNumber, DateTimeOffset expireDate)
        {
            _state = new CardState(cardId);
            
            _state.ApplyChange(new CreditCardCreatedEvent(
                PanEncryptor.EncryptPan(cardNumber),
                PanHasher.HashPan(cardNumber),
                expireDate));
            _state.ApplyChange(new CreditCardConnectedToAccountEvent(accountId));
        }

        public CreditCard(IEnumerable<CreditCardDomainEvent> historicEvents)
        {
            _state = new CardState(historicEvents);
        }
    }
}