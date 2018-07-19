namespace Bank.Cards.Domain.Account.Events
{
    [EventType("AccountCredited")]
    public class AccountCreditedEvent : AccountDomainEvent
    {
        public decimal Amount { get; }

        public string Reference { get; set; }

        public AccountCreditedEvent(decimal amount)
        {
            Amount = amount;
        }
    }
}