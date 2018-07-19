using System.Threading.Tasks;
using Bank.Cards.Domain.Card.ValueTypes;

namespace Bank.Cards.Domain.Card.Repositories
{
    public interface ICreditCardRootRepository
    {
        Task<CreditCard> GetCardById(CardId cardId);

        Task SaveCard(CreditCard card);
    }
}