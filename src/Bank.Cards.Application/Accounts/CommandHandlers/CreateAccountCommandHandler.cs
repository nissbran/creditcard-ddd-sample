using System.Threading.Tasks;
using Bank.Cards.Application.Accounts.Commands;
using Bank.Cards.Domain.Account;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Account.Services;

namespace Bank.Cards.Application.Accounts.CommandHandlers
{
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand>
    {
        private readonly IAccountRootRepository _accountRepository;
        private readonly AccountNumberGeneratorService _accountNumberGeneratorService;

        public CreateAccountCommandHandler(IAccountRootRepository accountRepository, AccountNumberGeneratorService accountNumberGeneratorService)
        {
            _accountRepository = accountRepository;
            _accountNumberGeneratorService = accountNumberGeneratorService;
        }

        public async Task<CommandExecutionResult> Handle(CreateAccountCommand command)
        {
            var accountNumber = _accountNumberGeneratorService.GenerateAccountNumber();

            var newAccount = new Account(command.AccountId, accountNumber);
            
            newAccount.SetCreditLimit(command.CreditLimit);

            await _accountRepository.SaveAccount(newAccount);
            
            return CommandExecutionResult.Ok;
        }
    }
}