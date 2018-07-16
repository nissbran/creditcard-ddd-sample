using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bank.Cards.Domain;

namespace Bank.Cards.Infrastructure.Serialization.Schemas
{
    public abstract class EventSchema<TBaseEvent> : IEventSchema where TBaseEvent : IDomainEvent
    {
        private readonly Dictionary<string, Type> _definitionToType = new Dictionary<string, Type>();
        private readonly Dictionary<Type, EventType> _typeToDefinition = new Dictionary<Type, EventType>();

        public abstract string Name { get; }

        protected EventSchema()
        {
            var baseEvent = typeof(TBaseEvent);
            var types = baseEvent.GetTypeInfo().Assembly.GetTypes()
                .Where(p => baseEvent.IsAssignableFrom(p));

            foreach (var type in types)
            {
                if (type.GetTypeInfo().GetCustomAttribute(typeof(EventTypeAttribute)) is EventTypeAttribute eventType)
                {
                    _definitionToType.Add(new EventType(eventType.Name, eventType.Version), type);
                    _typeToDefinition.Add(type, new EventType(eventType.Name, eventType.Version));
                }
            }
        }

        public Type GetDomainEventType(string eventType)
        {
            var eventDefinition = new EventType(eventType);

            if (_definitionToType.TryGetValue(eventDefinition.EventName, out var domainEvent))
                return domainEvent;

            return null;
        }

        public EventType GetEventType(IDomainEvent domainEvent)
        {
            if (_typeToDefinition.TryGetValue(domainEvent.GetType(), out var eventDefinition))
                return eventDefinition;

            throw new NotImplementedException();
        }
    }
}