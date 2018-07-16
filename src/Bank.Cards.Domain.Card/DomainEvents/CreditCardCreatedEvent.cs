namespace Bank.Cards.Domain.Card.Events
{
    using System;

    [EventType("CreditCardCreated")]
    public class CreditCardCreatedEvent : CreditCardDomainEvent
    {
        public string EncryptedPan { get; set; }

        public DateTimeOffset ExpiryDate { get; set; }
    }
}