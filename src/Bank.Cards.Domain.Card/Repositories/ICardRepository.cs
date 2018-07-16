using System.Threading.Tasks;

namespace Bank.Cards.Domain.Card.Repositories
{
    public interface ICreditCardDomainRepository
    {
        Task<Card> GetCardByHashedPan(string hashedPan);

        Task SaveCard(Card card);
    }
}