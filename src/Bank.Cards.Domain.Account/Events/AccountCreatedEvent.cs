namespace Bank.Cards.Domain.Account.Events
{
    [EventType("AccountCreated")]
    public class AccountCreatedEvent : AccountDomainEvent
    {
        public string CurrencyIso { get; set; }

        public string AccountNumber { get; }

        public AccountCreatedEvent(string accountNumber)
        {
            AccountNumber = accountNumber;
        }
    }
}