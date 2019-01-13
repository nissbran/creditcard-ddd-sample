using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Events
{
    [EventType("AccountCreated")]
    public class AccountCreatedEvent : AccountDomainEvent
    {
        public Country Country { get; }
        
        public Currency Currency { get; }

        public string AccountNumber { get; }

        public AccountCreatedEvent(Country country, Currency currency, string accountNumber)
        {
            Country = country;
            Currency = currency;
            AccountNumber = accountNumber;
        }
    }
}