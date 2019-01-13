using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Events
{
    [EventType("AccountCredited")]
    public class AccountCreditedEvent : AccountDomainEvent
    {
        public Money Amount { get; }

        public string Reference { get; set; }

        public AccountCreditedEvent(Money amount)
        {
            Amount = amount;
        }
    }
}