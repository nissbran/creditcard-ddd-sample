using System;
using System.Threading.Tasks;
using Bank.Cards.Domain.Account.ValueTypes;

namespace Bank.Cards.Domain.Account.Repositories
{
    public interface IAccountRootRepository
    {
        Task<Account> GetAccountById(AccountId accountId);

        Task SaveAccount(Account account);
    }
}