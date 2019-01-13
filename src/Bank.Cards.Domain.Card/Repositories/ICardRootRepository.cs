using System.Threading.Tasks;
using Bank.Cards.Domain.Card.ValueTypes;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Card.Repositories
{
    public interface ICreditCardRootRepository
    {
        Task<CreditCard> GetCardById(CardId cardId);

        Task SaveCard(CreditCard card);
    }
}