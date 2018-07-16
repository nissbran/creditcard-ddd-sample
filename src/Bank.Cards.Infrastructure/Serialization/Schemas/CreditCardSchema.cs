using Bank.Cards.Domain.Card.Events;

namespace Bank.Cards.Infrastructure.Serialization.Schemas
{
    public class CreditCardSchema : EventSchema<CreditCardDomainEvent>
    {
        public const string SchemaName = "CreditCard";

        public override string Name => SchemaName;
    }
}