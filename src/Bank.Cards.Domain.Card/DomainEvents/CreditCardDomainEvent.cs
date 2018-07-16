using System.Runtime.Serialization;

namespace Bank.Cards.Domain.Card.Events
{
    public abstract class CreditCardDomainEvent : IDomainEvent
    {
        [IgnoreDataMember]
        public string AggregateType => "CreditCard";

        [IgnoreDataMember]
        public string AggregateId { get; set; }
        
        [IgnoreDataMember]
        public long EventNumber { get; set; }
    }
}