namespace Bank.Cards.Domain
{
    public abstract class DomainEvent
    {   
        public abstract string AggregateType { get; }
        
        public abstract string AggregateId { get; set; }
        
        public abstract long EventNumber { get; set; }
    }
}