using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Model;
using Xunit;

namespace Bank.Cards.Domain.Account.UnitTest.Tests
{
    public class AccountTests
    {
        [Fact]
        public void When_account_is_created_Then_correct_id_is_set()
        {
            // Arrange
            var accountId = AccountId.NewId();
            var accountNumber = new AccountNumber("41040123");
            
            // Act
            var account = new Account(accountId, Country.Sweden, Currency.SEK, accountNumber);
            
            // Assert
            Assert.Equal(accountId, account.Id);
        }
        
        [Fact]
        public void Given_account_with_balance_close_to_credit_limit_When_debit_is_over_limit_Then_credit_limit_hit_event_is_set()
        {
            // Arrange
            var account = GivenAccount()
                .With(new CreditLimitChangedEvent(5000))
                .With(new AccountDebitedEvent(Money.Create(4500, Currency.SEK)))
                .Build();
            
            // Act
            account.Debit(Money.Create(600, Currency.SEK), new TransactionReference("Ref"));
            
            // Assert
            account.AssertNewEventExist<CreditLimitHitEvent>();
            account.AssertNewEventDoesNotExist<AccountDebitedEvent>();
        }

        private static IAccountBuilder GivenAccount()
        {
            return new AccountBuilder();
        }
    }
}