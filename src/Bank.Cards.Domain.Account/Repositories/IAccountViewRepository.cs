using System.Threading.Tasks;
using Bank.Cards.Domain.Account.ValueTypes;
using Bank.Cards.Domain.Account.Views;

namespace Bank.Cards.Domain.Account.Repositories
{
    public interface IAccountViewRepository
    {
        Task<AccountBalanceView> GetAccountBalance(AccountId id);
        Task<AccountStatusView> GetAccountStatus(AccountId id);
    }
}