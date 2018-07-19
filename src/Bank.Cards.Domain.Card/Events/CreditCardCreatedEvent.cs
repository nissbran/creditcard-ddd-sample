namespace Bank.Cards.Domain.Card.Events
{
    using System;

    [EventType("CreditCardCreated")]
    public class CreditCardCreatedEvent : CreditCardDomainEvent
    {
        public string EncryptedPan { get; }
        
        public string HashedPan { get; }

        public DateTimeOffset ExpiryDate { get; }

        public CreditCardCreatedEvent(string encryptedPan, string hashedPan, DateTimeOffset expiryDate)
        {
            EncryptedPan = encryptedPan;
            ExpiryDate = expiryDate;
            HashedPan = hashedPan;
        }
    }
}