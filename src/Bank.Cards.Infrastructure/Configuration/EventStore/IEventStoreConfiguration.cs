namespace Bank.Cards.Infrastructure.Configuration.EventStore
{
    public interface IEventStoreConfiguration
    {
        bool UseSingleNode { get; }

        string SingleNodeConnectionUri { get; }

        IEventStoreClusterConfiguration ClusterConfiguration { get; }
    }
}