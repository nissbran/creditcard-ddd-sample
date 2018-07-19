using Bank.Cards.Domain.Account.Events;
using Bank.Cards.Domain.Account.UnitTest.Tests;

namespace Bank.Cards.Domain.Account.UnitTest
{
    public static class AccountBuilderExtensions
    {
        public static IAccountBuilder With(this IAccountBuilder builder, AccountDomainEvent domainEvent)
        {
            builder.DomainEvents.Add(domainEvent);

            return builder;
        }
    }
}