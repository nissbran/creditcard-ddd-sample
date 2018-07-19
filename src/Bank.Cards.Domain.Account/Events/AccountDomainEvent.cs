using System.Runtime.Serialization;

namespace Bank.Cards.Domain.Account.Events
{
    public abstract class AccountDomainEvent : DomainEvent
    {
        [IgnoreDataMember]
        public override string AggregateType => "Account";

        [IgnoreDataMember]
        public override string AggregateId { get; set; }

        [IgnoreDataMember]
        public override long EventNumber { get; set; }
    }
}