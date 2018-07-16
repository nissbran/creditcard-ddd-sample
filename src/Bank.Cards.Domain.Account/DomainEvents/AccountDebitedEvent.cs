namespace Bank.Cards.Domain.Account.Events
{
    [EventType("AccountDebited")]
    public class AccountDebitedEvent : AccountDomainEvent
    {
        public decimal Amount { get; }

        public decimal VatAmount { get; }

        public string Reference { get; set; }

        public string CreatedBy { get; set; }

        public AccountDebitedEvent(decimal amount, decimal vatAmount)
        {
            Amount = amount;
            VatAmount = vatAmount;
        }
    }
}