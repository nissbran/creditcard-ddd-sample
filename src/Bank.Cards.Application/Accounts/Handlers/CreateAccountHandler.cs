using System.Threading.Tasks;
using Bank.Cards.Domain.Account;
using Bank.Cards.Domain.Account.Commands;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Account.Services;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Application.Accounts.Handlers
{
    public class CreateAccountHandler : CommandHandler<CreateAccount>
    {
        private readonly IAccountRootRepository _accountRepository;
        private readonly AccountNumberGeneratorService _accountNumberGeneratorService;

        public CreateAccountHandler(IAccountRootRepository accountRepository, AccountNumberGeneratorService accountNumberGeneratorService)
        {
            _accountRepository = accountRepository;
            _accountNumberGeneratorService = accountNumberGeneratorService;
        }

        public override async Task<CommandExecutionResult> Handle(CreateAccount command)
        {
            var accountNumber = _accountNumberGeneratorService.GenerateAccountNumber();

            var newAccount = new Account(command.AccountId, command.Country, command.Currency, accountNumber);
            
            newAccount.SetCreditLimit(command.CreditLimit);

            await _accountRepository.SaveAccount(newAccount);
            
            return Ok();
        }
    }
}