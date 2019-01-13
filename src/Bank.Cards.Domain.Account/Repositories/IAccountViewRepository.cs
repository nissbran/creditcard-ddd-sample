using System.Threading.Tasks;
using Bank.Cards.Domain.Account.Views;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Repositories
{
    public interface IAccountViewRepository
    {
        Task<AccountBalanceView> GetAccountBalance(AccountId id);
        Task<AccountStatusView> GetAccountStatus(AccountId id);
    }
}