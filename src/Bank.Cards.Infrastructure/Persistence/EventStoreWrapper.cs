﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Cards.Domain;
using Bank.Cards.Infrastructure.Persistence.EventStore;
using Bank.Cards.Infrastructure.Serialization;
using EventStore.ClientAPI;

namespace Bank.Cards.Infrastructure.Persistence
{
    public class EventStoreWrapper : IEventStore
    {
        private const int ReadBatchSize = 2000;

        private readonly IEventStoreConnection _connection;
        private readonly EventSerializer _eventSerializer;

        public EventStoreWrapper(IEventStoreConnection connection, EventSerializer eventSerializer)
        {
            _connection = connection;
            _eventSerializer = eventSerializer;
        }

        public async Task<IList<IDomainEvent>> GetEventsByStreamId(EventStreamId eventStreamId)
        {
            return await GetEventsFromStreamVersion(eventStreamId.StreamName, StreamPosition.Start, eventStreamId.ResolveLinks);
        }

        private async Task<IList<IDomainEvent>> GetEventsFromStreamVersion(string streamName, long streamVersion, bool resolveLinks)
        {
            var streamEvents = new List<ResolvedEvent>();

            StreamEventsSlice currentSlice;
            var nextSliceStart = streamVersion;
            do
            {
                currentSlice = await _connection.ReadStreamEventsForwardAsync(
                    stream: streamName,
                    start: nextSliceStart,
                    count: ReadBatchSize,
                    resolveLinkTos: resolveLinks);

                nextSliceStart = currentSlice.NextEventNumber;

                streamEvents.AddRange(currentSlice.Events);

            } while (!currentSlice.IsEndOfStream);

            return streamEvents.Select(ConvertEventDataToDomainEvent).ToList();
        }

        public async Task<StreamWriteResult> SaveEvents(EventStreamId eventStreamId, long streamVersion, List<IDomainEvent> events)
        {
            if (events.Any() == false)
                return new StreamWriteResult(-1);

            var commitId = Guid.NewGuid();

            var expectedVersion = streamVersion == 0 ? ExpectedVersion.NoStream : streamVersion - 1;
            var eventsToSave = events.Select(domainEvent => ToEventData(commitId, domainEvent));

            var result = await _connection.AppendToStreamAsync(
                stream: eventStreamId.ToString(),
                expectedVersion: expectedVersion,
                events: eventsToSave);

            return new StreamWriteResult(result.NextExpectedVersion);
        }

        private IDomainEvent ConvertEventDataToDomainEvent(ResolvedEvent resolvedEvent)
        {
            return _eventSerializer.DeserializeEvent(resolvedEvent);
        }

        private EventData ToEventData(Guid commitId, IDomainEvent domainEvent)
        {
            return _eventSerializer.SerializeDomainEvent(commitId, domainEvent);
        }
    }
}