namespace Bank.Cards.Domain.Account
{
    public sealed class AccountNumber
    {
        private readonly string _number;
        
        public AccountNumber(string number)
        {
            _number = number;
        }
        
        public static implicit operator string(AccountNumber accountNumber)
        {
            return accountNumber._number;
        }
    }
}