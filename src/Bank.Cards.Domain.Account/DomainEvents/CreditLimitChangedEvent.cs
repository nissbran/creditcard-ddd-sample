namespace Bank.Cards.Domain.Account.Events
{
    [EventType("CreditLimitSet")]
    public class CreditLimitChangedEvent : AccountDomainEvent
    {
        public decimal CreditLimit { get; set; }
    }
}