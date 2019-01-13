using Bank.Cards.Domain.Model;

namespace Bank.Cards.Domain.Account.Queries
{
    public class GetAccountBalanceQuery
    {
        public AccountId AccountId { get; set; }
    }
}