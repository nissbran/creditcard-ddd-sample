namespace Bank.Cards.Domain.Account.ValueTypes
{
    public sealed class TransactionReference
    {
        private readonly string _reference;
        
        public TransactionReference(string reference)
        {
            _reference = reference;
        }
        
        public static implicit operator string(TransactionReference transactionReference)
        {
            return transactionReference._reference;
        }
    }
}