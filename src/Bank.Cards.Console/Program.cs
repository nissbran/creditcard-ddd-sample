using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Bank.Cards.Domain.Account;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Enumerations;
using Bank.Cards.Domain.ValueTypes;
using Bank.Cards.Infrastructure.Configuration.EventStore;
using Bank.Cards.Infrastructure.Persistence;
using Bank.Cards.Infrastructure.Repositories;
using Bank.Cards.Infrastructure.Serialization;
using Bank.Cards.Infrastructure.Serialization.Schemas;

namespace Bank.Cards.Console
{
    public class Program
    {
        private const int NumberOfAggregates = 5;

        private static IAccountRootRepository _accountRootRepository;
        private static IAccountViewRepository _accountViewsRepository;
        
        public static async Task Main(string[] args)
        {
            var connection = EventStoreConnectionFactory.Create(new EventStoreSingleNodeConfiguration(), "admin", "changeit");

            await connection.ConnectAsync();
            
            var eventStore = new EventStoreWrapper(connection, new JsonEventSerializer(
                new List<IEventSchema>
                {
                    new AccountSchema()
                }));

            _accountRootRepository = new AccountRootRepository(eventStore);
            _accountViewsRepository = new AccountViewRepository(eventStore);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var tasks = new Task[NumberOfAggregates];

            for (var i = 0; i < NumberOfAggregates; i++)
            {
                var number = i;
                tasks[i] = Task.Run(async () => { await CreateAccount(number); });
            }

            await Task.WhenAll(tasks);
            System.Console.WriteLine($"Time: {stopwatch.Elapsed}");

            for (var i = 0; i < NumberOfAggregates; i++)
            {
                var number = i;
                tasks[i] = Task.Run(async () => { await GetAccountBalances(number); });
            }
            
            await Task.WhenAll(tasks);


            System.Console.ReadLine();
        }
        
        private static async Task CreateAccount(int number)
        {
            var id = Guid.Parse($"42a11f29-4578-4d19-b1ec-544260ea40{number:D2}");

            for (int i = 0; i < 200; i++)
            {
                var account = await _accountRootRepository.GetAccountById(id);

                if (account == null)
                {
                    account = new Account(id, "Test");
                }

                for (int y = 0; y < 100; y++)
                {
                    account.DebitAccount(new Money(7.5m, 2.5m, Currency.SEK));
                }
                
                await _accountRootRepository.SaveAccount(account);
            }
        }

        private static async Task GetAccountBalances(int number)
        {
            var id = Guid.Parse($"42a11f29-4578-4d19-b1ec-544260ea40{number:D2}");

            var balance = await _accountViewsRepository.GetAccountBalance(id);
            
            System.Console.WriteLine($"{number}: {balance.Balance}");
        }

    }
}