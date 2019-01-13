using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Commands
{
    public class DebitAccount : Command
    {
        public AccountId AccountId { get; set; }

        public Money AmountToDebit { get; set; }
        
        public TransactionReference Reference { get; set; }
    }
}