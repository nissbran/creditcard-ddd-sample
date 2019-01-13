using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Card.Commands
{
    public class CreateCreditCardForAccount : Command
    {
        public AccountId AccountId { get; }
        
        public CardId NewCardId { get; }

        public CreateCreditCardForAccount(AccountId accountId, CardId newCardId)
        {
            AccountId = accountId;
            NewCardId = newCardId;
        }
    }
}