﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Cards.Domain;

namespace Bank.Cards.Infrastructure.Persistence.EventStore
{
    public interface IEventStore
    {
        Task<IList<DomainEvent>> GetEventsByStreamId(EventStreamId eventStreamId);

        Task<StreamWriteResult> SaveEvents(EventStreamId eventStreamId, long streamVersion, List<DomainEvent> events);
    }
}