using Bank.Cards.Domain.Account.ValueTypes;

namespace Bank.Cards.Application.Accounts.Commands
{
    public class CreateAccountCommand : Command
    {
        public AccountId AccountId { get; set; }
        
        public decimal CreditLimit { get; set; }
    }
}