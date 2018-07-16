using System.Collections.Generic;

namespace Bank.Cards.Infrastructure.Configuration.EventStore
{
    public interface IEventStoreClusterConfiguration
    {
        bool UseSsl { get; }

        IEnumerable<IEventStoreClusterNode> ClusterNodes { get; }
    }
}