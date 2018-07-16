namespace Bank.Cards.Domain.Card.Events
{
    [EventType("CreditCardDetailsSet")]
    public class CreditCardDetailsSetEvent : CreditCardDomainEvent
    {
        public string NameOnCard { get; set; }
    }
}