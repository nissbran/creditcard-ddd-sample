namespace Bank.Cards.Domain.Account.Events
{
    [EventType("CreditLimitChanged")]
    public class CreditLimitChangedEvent : AccountDomainEvent
    {
        public decimal CreditLimit { get; }

        public CreditLimitChangedEvent(decimal creditLimit)
        {
            CreditLimit = creditLimit;
        }
    }
}