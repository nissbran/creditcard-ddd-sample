using Bank.Cards.Domain.Account.ValueTypes;

namespace Bank.Cards.Application.Accounts.Queries
{
    public class GetAccountBalanceQuery
    {
        public AccountId AccountId { get; set; }
    }
}