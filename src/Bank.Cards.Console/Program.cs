using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Bank.Cards.Application;
using Bank.Cards.Application.Accounts.CommandHandlers;
using Bank.Cards.Application.Accounts.Commands;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Account.Services;
using Bank.Cards.Domain.Account.ValueTypes;
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

        private static ICommandHandler<CreateAccountCommand> _createAccountCommandHandler;
        private static ICommandHandler<DebitAccountCommand> _debitAccountCommandHandler;
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

            var accountRootRepository = new AccountRootRepository(eventStore);
            _createAccountCommandHandler = new CreateAccountCommandHandler(accountRootRepository, new AccountNumberGeneratorService());
            _debitAccountCommandHandler = new DebitAccountCommandHandler(accountRootRepository);
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
            var id = AccountId.Parse($"42a11f29-4578-4d19-b1ec-544260ea40{number:D2}");

            var account = await _accountViewsRepository.GetAccountStatus(id);

            if (account == null)
            {
                await _createAccountCommandHandler.Handle(new CreateAccountCommand
                {
                    AccountId = id,
                    CreditLimit = 15000
                });
            }

            for (int i = 0; i < 200; i++)
            {
                await _debitAccountCommandHandler.Handle(new DebitAccountCommand
                {
                    AccountId = id,
                    Reference = new TransactionReference($"Ref_{i}"),
                    AmountToDebit = new Money(100m, Currency.SEK)
                });
            }
        }

        private static async Task GetAccountBalances(int number)
        {
            var id = AccountId.Parse($"42a11f29-4578-4d19-b1ec-544260ea40{number:D2}");

            var balance = await _accountViewsRepository.GetAccountBalance(id);
            
            System.Console.WriteLine($"{number}: {balance.Balance}");
        }

    }
}