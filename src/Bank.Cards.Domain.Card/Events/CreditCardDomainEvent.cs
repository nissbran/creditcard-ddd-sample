using System.Runtime.Serialization;

namespace Bank.Cards.Domain.Card.Events
{
    public abstract class CreditCardDomainEvent : DomainEvent
    {
        [IgnoreDataMember]
        public override string AggregateType => "CreditCard";

        [IgnoreDataMember]
        public override string AggregateId { get; set; }
        
        [IgnoreDataMember]
        public override long EventNumber { get; set; }
    }
}