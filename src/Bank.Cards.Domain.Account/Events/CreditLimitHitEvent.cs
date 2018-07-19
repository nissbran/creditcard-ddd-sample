namespace Bank.Cards.Domain.Account.Events
{
    [EventType("CreditLimitHit")]
    public class CreditLimitHitEvent : AccountDomainEvent
    {
        public string TransactionReference { get; }

        public CreditLimitHitEvent(string transactionReference)
        {
            TransactionReference = transactionReference;
        }
    }
}