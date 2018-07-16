namespace Bank.Cards.Domain
{
    public interface IDomainEvent
    {   
        string AggregateType { get; }
        
        string AggregateId { get; set; }
        
        long EventNumber { get; set; }
    }
}