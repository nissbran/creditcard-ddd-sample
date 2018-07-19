using System.Linq;
using Bank.Cards.Domain.Account.Events;
using Xunit;

namespace Bank.Cards.Domain.Account.UnitTest
{
    public static class AccountAssertionExtensions
    {
        public static void AssertNewEventExist<T>(this Account account) where T : AccountDomainEvent
        {
            Assert.True(account.UncommittedEvents.Any(a => a is T));
        }

        public static void AssertNewEventDoesNotExist<T>(this Account account) where T : AccountDomainEvent
        {
            Assert.False(account.UncommittedEvents.Any(a => a is T));
        }
    }
}