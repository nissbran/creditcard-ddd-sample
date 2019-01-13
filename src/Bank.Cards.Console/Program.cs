using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bank.Cards.Application;
using Bank.Cards.Application.Accounts.Handlers;
using Bank.Cards.Domain.Account;
using Bank.Cards.Domain.Account.Commands;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Account.Services;
using Bank.Cards.Domain.Model;
using Bank.Cards.Infrastructure.Configuration.EventStore;
using Bank.Cards.Infrastructure.Persistence;
using Bank.Cards.Infrastructure.Repositories;
using Bank.Cards.Infrastructure.Serialization;
using Bank.Cards.Infrastructure.Serialization.Schemas;

namespace Bank.Cards.Console
{
    public class Program
    {
        private const int NumberOfAggregates = 20;

        private static ICommandHandler<CreateAccount> _createAccountCommandHandler;
        private static ICommandHandler<DebitAccount> _debitAccountCommandHandler;
        private static IAccountViewRepository _accountViewRepository;
        
        public static async Task Main(string[] args)
        {
//            var tasks = new Task[NumberOfAggregates];
//
//            for (var i = 0; i < NumberOfAggregates; i++)
//            {
//                var number = i;
//                tasks[i] = Task.Run(async () =>
//                {
//                    using (var client = new HttpClient())
//                    {
//                        client.BaseAddress = new Uri("http://localhost:5000");
//
//                        for (int j = 0; j < 100000; j++)
//                        {
//                            await client.PostAsync("cards", new StringContent("{\"name\":\"" + number + ":" + j+ "\"}", Encoding.UTF8, "application/json"));
//                        }
//                    }
//                });
//            }
//
//            await Task.WhenAll(tasks);
            
            var connection = EventStoreConnectionFactory.Create(new EventStoreSingleNodeConfiguration(), "admin", "changeit");

            await connection.ConnectAsync();
            
            var eventStore = new EventStoreWrapper(connection, new JsonEventSerializer(
                new List<IEventSchema>
                {
                    new AccountSchema()
                }));

            var accountRootRepository = new AccountRootRepository(eventStore);
            _createAccountCommandHandler = new CreateAccountHandler(accountRootRepository, new AccountNumberGeneratorService());
            _debitAccountCommandHandler = new DebitAccountHandler(accountRootRepository);
            _accountViewRepository = new AccountViewRepository(eventStore);

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
            var id = AccountId.Parse($"42a11f29-4578-4d19-b1ec-544260ea40{number:D2}");

            var account = await _accountViewRepository.GetAccountStatus(id);

            if (account == null)
            {
                await _createAccountCommandHandler.Handle(new CreateAccount(id, Country.Sweden, Currency.SEK)
                {
                    CreditLimit = 15000
                });
            }

            for (int i = 0; i < 200; i++)
            {
                await _debitAccountCommandHandler.Handle(new DebitAccount
                {
                    AccountId = id,
                    Reference = new TransactionReference($"Ref_{i}"),
                    AmountToDebit = Money.Create(100m, Currency.SEK)
                });
            }
        }

        private static async Task GetAccountBalances(int number)
        {
            var id = AccountId.Parse($"42a11f29-4578-4d19-b1ec-544260ea40{number:D2}");

            var balance = await _accountViewRepository.GetAccountBalance(id);
            
            System.Console.WriteLine($"{number}: {balance.Balance}");
        }

    }
}