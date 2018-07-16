namespace Bank.Cards.Domain.Card.Events
{
    using System;

    [EventType("CreditCardConnectedToAccount")]
    public class CreditCardConnectedToAccountEvent : CreditCardDomainEvent
    {
        public Guid AccountId { get; set; }
    }
}