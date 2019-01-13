using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Commands
{
    public class CreateAccount : Command
    {
        public AccountId AccountId { get; }
        
        public Country Country { get; }
        
        public Currency Currency { get; }
        
        public decimal CreditLimit { get; set; }

        public CreateAccount(AccountId accountId, Country country, Currency currency)
        {
            AccountId = accountId;
            Country = country;
            Currency = currency;
        }
    }
}