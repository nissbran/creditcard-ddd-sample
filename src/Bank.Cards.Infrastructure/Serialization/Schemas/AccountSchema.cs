using Bank.Cards.Domain.Account.Events;

namespace Bank.Cards.Infrastructure.Serialization.Schemas
{
    public class AccountSchema : EventSchema<AccountDomainEvent>
    {
        public const string SchemaName = "Account";

        public override string Name => SchemaName;
    }
}