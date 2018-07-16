namespace Bank.Cards.Domain.Account.Events
{
    [EventType("IssuerInformationSet")]
    public class IssuerInformationSetEvent : AccountDomainEvent
    {
        public long IssuerId { get; set; }
    }
}