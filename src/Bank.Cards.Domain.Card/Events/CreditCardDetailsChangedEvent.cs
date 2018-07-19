namespace Bank.Cards.Domain.Card.Events
{
    [EventType("CreditCardDetailsChangedEvent")]
    public class CreditCardDetailsChangedEvent : CreditCardDomainEvent
    {
        public string NameOnCard { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}