using System.Collections.Generic;

namespace Bank.Cards.Domain
{
    public interface IAggregateRoot
    {
        long AggregateVersion { get; }

        List<DomainEvent> UncommittedEvents { get; }
    }
}