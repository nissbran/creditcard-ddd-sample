using System.Collections.Generic;

namespace Bank.Cards.Domain
{
    public abstract class AggregateRoot<TId, TState> where TState : AggregateState<TId>
    {
        public long AggregateVersion => State.Version;
        
        public List<DomainEvent> UncommittedEvents { get; } = new List<DomainEvent>();

        protected TState State { get; }

        protected AggregateRoot(TState state)
        {
            State = state;
        }

        public virtual long GetExpectedVersion()
        {
            return AggregateVersion - UncommittedEvents.Count;
        }
        
        protected void ApplyChange(DomainEvent domainEvent)
        {
            State.ApplyEvent(domainEvent);
            UncommittedEvents.Add(domainEvent);
        }
    }

    public abstract class AggregateState<TId> : AggregateState
    {
        public TId Id { get; }

        protected AggregateState(TId id)
        {
            Id = id;
        }
    }

    public abstract class AggregateState
    {
        public long Version { get; protected set; }
        public abstract void ApplyEvent(DomainEvent domainEvent);
    }
}