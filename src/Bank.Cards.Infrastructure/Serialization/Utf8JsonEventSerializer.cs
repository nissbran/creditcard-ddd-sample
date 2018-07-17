using System;
using System.Collections.Generic;
using Bank.Cards.Domain;
using Bank.Cards.Infrastructure.Serialization.Schemas;
using EventStore.ClientAPI;
using Utf8Json.Resolvers;
using Utf8Json;

namespace Bank.Cards.Infrastructure.Serialization
{
    public class Utf8JsonEventSerializer : IEventSerializer
    {
        private readonly Dictionary<string, IEventSchema> _eventSchemas = new Dictionary<string, IEventSchema>();

        public Utf8JsonEventSerializer(IEnumerable<IEventSchema> eventSchemas)
        {
            foreach (var schema in eventSchemas)
            {
                _eventSchemas.Add(schema.Name, schema);
            }
            
            JsonSerializer.SetDefaultResolver(StandardResolver.AllowPrivateCamelCase);
        }
        
        public EventData SerializeDomainEvent(Guid commitId, IDomainEvent domainEvent)
        {
            _eventSchemas.TryGetValue(domainEvent.AggregateType, out var schema);

            var eventType = schema.GetEventType(domainEvent);
            var eventId = Guid.NewGuid();

            var data = JsonSerializer.NonGeneric.Serialize(domainEvent);
            var metadata = JsonSerializer.Serialize(new DomainMetadata
            {
                CorrelationId = commitId,
                AggregateRootId = domainEvent.AggregateId,
                EventTypeVersion = eventType.LatestVersion,
                Schema = domainEvent.AggregateType,
                Occurred = DateTimeOffset.UtcNow
            });
            
            return new EventData(eventId, eventType, true, data, metadata);
        }

        public IDomainEvent DeserializeEvent(ResolvedEvent resolvedEvent)
        {
            var metadata = JsonSerializer.Deserialize<DomainMetadata>(resolvedEvent.Event.Metadata);

            _eventSchemas.TryGetValue(metadata.Schema, out var schema);

            var eventType = schema.GetDomainEventType(resolvedEvent.Event.EventType);

            var domainEvent = (IDomainEvent)JsonSerializer.NonGeneric.Deserialize(eventType, resolvedEvent.Event.Data);
            domainEvent.AggregateId = metadata.AggregateRootId;

            return domainEvent;
        }
    }
}
