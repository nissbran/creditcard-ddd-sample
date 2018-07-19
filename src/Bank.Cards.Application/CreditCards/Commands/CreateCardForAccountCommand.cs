using Bank.Cards.Domain.Account.ValueTypes;
using Bank.Cards.Domain.Card.ValueTypes;

namespace Bank.Cards.Application.CreditCards.Commands
{
    public class CreateCardForAccountCommand : Command
    {
        public AccountId AccountId { get; set; }
        
        public CardId CardId { get; set; }
    }
}