namespace Bank.Cards.Domain.Card.Events
{
    [EventType("CreditCardOwnerDetailsChanged")]
    public class CreditCardOwnerDetailsChangedEvent : CreditCardDomainEvent
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}