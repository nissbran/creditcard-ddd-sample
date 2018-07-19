using System.Threading.Tasks;
using Bank.Cards.Application.Accounts.Commands;
using Bank.Cards.Domain.Account;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Account.Services;

namespace Bank.Cards.Application.Accounts.CommandHandlers
{
    public class DebitAccountCommandHandler : ICommandHandler<DebitAccountCommand>
    {
        private readonly IAccountRootRepository _accountRepository;

        public DebitAccountCommandHandler(IAccountRootRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommandExecutionResult> Handle(DebitAccountCommand command)
        {
            var account = await _accountRepository.GetAccountById(command.AccountId);
            
            account.Debit(command.AmountToDebit, command.Reference);
            
            await _accountRepository.SaveAccount(account);
            
            return CommandExecutionResult.Ok;
        }
    }
}