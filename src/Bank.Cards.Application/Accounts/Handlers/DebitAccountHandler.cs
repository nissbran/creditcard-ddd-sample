using System.Threading.Tasks;
using Bank.Cards.Domain.Account.Commands;
using Bank.Cards.Domain.Account.Repositories;

namespace Bank.Cards.Application.Accounts.Handlers
{
    public class DebitAccountHandler : CommandHandler<DebitAccount>
    {
        private readonly IAccountRootRepository _accountRepository;

        public DebitAccountHandler(IAccountRootRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public override async Task<CommandExecutionResult> Handle(DebitAccount command)
        {
            var account = await _accountRepository.GetAccountById(command.AccountId);
            
            account.Debit(command.AmountToDebit, command.Reference);
            
            await _accountRepository.SaveAccount(account);
            
            return Ok();
        }
    }
}