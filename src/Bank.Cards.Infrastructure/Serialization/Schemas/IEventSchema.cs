using System;
using Bank.Cards.Domain;

namespace Bank.Cards.Infrastructure.Serialization.Schemas
{
    public interface IEventSchema
    {
        string Name { get; }

        Type GetDomainEventType(string eventType);

        EventType GetEventType(IDomainEvent domainEvent);
    }
}