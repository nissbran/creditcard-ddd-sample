using System;
using System.Collections.Generic;
using Bank.Cards.Domain.Card.Events;
using Bank.Cards.Domain.Card.Projections;
using Bank.Cards.Domain.Card.Services;
using Bank.Cards.Domain.Card.ValueTypes;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Card
{
    public class CreditCard : AggregateRoot<CardId, CreditCardStateProjection>
    {
        public CardId Id => State.Id;

        private CreditCard(CreditCardStateProjection state) : base(state)
        {
        }

        public CreditCard(CardId id, AccountId accountId, CardNumber cardNumber, DateTimeOffset expireDate) : this(new CreditCardStateProjection(id))
        {
            ApplyChange(new CreditCardCreatedEvent(
                PanEncryptor.EncryptPan(cardNumber),
                PanHasher.HashPan(cardNumber),
                expireDate));
            ApplyChange(new CreditCardConnectedToAccountEvent(accountId));
        }

        public CreditCard(CardId id, IEnumerable<CreditCardDomainEvent> historicEvents) : this(
            new CreditCardStateProjection(id, historicEvents))
        {
        }
    }
}