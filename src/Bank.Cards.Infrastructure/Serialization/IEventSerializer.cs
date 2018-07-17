using System;
using Bank.Cards.Domain;
using EventStore.ClientAPI;

namespace Bank.Cards.Infrastructure.Serialization
{
    public interface IEventSerializer
    {
        EventData SerializeDomainEvent(Guid commitId, IDomainEvent domainEvent);
        IDomainEvent DeserializeEvent(ResolvedEvent resolvedEvent);
    }
}