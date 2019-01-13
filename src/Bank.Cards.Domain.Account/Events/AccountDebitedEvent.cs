using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Events
{
    [EventType("AccountDebited")]
    public class AccountDebitedEvent : AccountDomainEvent
    {
        public Money Amount { get; }

        public string Reference { get; set; }

        public AccountDebitedEvent(Money amount)
        {
            Amount = amount;
        }
    }
}