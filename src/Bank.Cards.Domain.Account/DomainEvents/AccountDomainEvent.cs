using System.Runtime.Serialization;

namespace Bank.Cards.Domain.Account.Events
{
    public abstract class AccountDomainEvent : IDomainEvent
    {
        [IgnoreDataMember]
        public string AggregateType => "Account";

        [IgnoreDataMember]
        public string AggregateId { get; set; }

        [IgnoreDataMember]
        public long EventNumber { get; set; }
    }
}