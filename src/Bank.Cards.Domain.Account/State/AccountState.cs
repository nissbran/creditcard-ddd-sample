namespace Bank.Cards.Domain.Account.State
{
    public class AccountState
    {
        public string AccountId { get; internal set; }
        
        public string AccountNumber { get; internal set; }

        public decimal Balance { get; internal set; }

        public long IssuerId { get; internal set; }

        public decimal CreditLimit { get; internal set; }
    }
}