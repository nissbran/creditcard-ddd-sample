using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Card.Events
{
    [EventType("CreditCardConnectedToAccount")]
    public class CreditCardConnectedToAccountEvent : CreditCardDomainEvent
    {
        public AccountId AccountId { get; }

        public CreditCardConnectedToAccountEvent(AccountId accountId)
        {
            AccountId = accountId;
        }
    }
}