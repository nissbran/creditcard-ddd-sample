using System;
using System.Threading.Tasks;

namespace Bank.Cards.Domain.Account.Repositories
{
    public interface IAccountRootRepository
    {
        Task<Account> GetAccountById(Guid accountId);

        Task SaveAccount(Account account);
    }
}