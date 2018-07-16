using System;
using System.Threading.Tasks;
using Bank.Cards.Domain.Account.Views;

namespace Bank.Cards.Domain.Account.Repositories
{
    public interface IAccountViewRepository
    {
        Task<AccountBalanceView> GetAccountBalance(Guid id);
    }
}