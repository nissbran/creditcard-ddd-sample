﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace Bank.Cards.Infrastructure.Configuration.EventStore
{
    public static class EventStoreConnectionFactory
    {
        public static IEventStoreConnection Create(IEventStoreConfiguration configuration, string username, string password, ILogger customLogger = null)
        {
            var connectionSettings = ConnectionSettings.Create()
                .FailOnNoServerResponse()
                .KeepReconnecting()
                .KeepRetrying()
                .SetMaxDiscoverAttempts(int.MaxValue)
                .SetDefaultUserCredentials(new UserCredentials(username, password));

            if (customLogger != null)
                connectionSettings.UseCustomLogger(customLogger);
            
            if (configuration.UseSingleNode)
                return EventStoreConnection.Create(connectionSettings.Build(), new Uri(configuration.SingleNodeConnectionUri));

            var gossipEndpoints = BuildIpEndPoints(configuration.ClusterConfiguration.ClusterNodes).ToArray();

            connectionSettings.SetGossipSeedEndPoints(gossipEndpoints);

            if (configuration.ClusterConfiguration.UseSsl)
            {
                // This host name does not need to exist. It's used only to enable server validation in terms of matching certificate and trust chain.
                connectionSettings.UseSslConnection("your-domain.com", validateServer: true);
            }

            return EventStoreConnection.Create(connectionSettings.Build());
        }

        private static IEnumerable<IPEndPoint> BuildIpEndPoints(IEnumerable<IEventStoreClusterNode> clusterNodes)
        {
            var gossipHosts = new List<IPEndPoint>();

            foreach (var clusterNode in clusterNodes)
            {
                if (clusterNode.HostNameSpecified)
                    gossipHosts.Add(new IPEndPoint(Dns.GetHostEntryAsync(clusterNode.HostName).Result.AddressList[0], clusterNode.ExternalPort));
                else
                    gossipHosts.Add(new IPEndPoint(IPAddress.Parse(clusterNode.IpAddress), clusterNode.ExternalPort));
            }

            return gossipHosts;
        }
    }
}
