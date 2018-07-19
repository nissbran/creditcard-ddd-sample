namespace Bank.Cards.Domain.Account.Events
{
    [EventType("AccountDebited")]
    public class AccountDebitedEvent : AccountDomainEvent
    {
        public decimal Amount { get; }

        public string Reference { get; set; }

        public AccountDebitedEvent(decimal amount)
        {
            Amount = amount;
        }
    }
}