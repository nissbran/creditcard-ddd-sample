using Bank.Cards.Domain.Account.ValueTypes;
using Bank.Cards.Domain.ValueTypes;

namespace Bank.Cards.Application.Accounts.Commands
{
    public class DebitAccountCommand : Command
    {
        public AccountId AccountId { get; set; }

        public Money AmountToDebit { get; set; }
        
        public TransactionReference Reference { get; set; }
    }
}