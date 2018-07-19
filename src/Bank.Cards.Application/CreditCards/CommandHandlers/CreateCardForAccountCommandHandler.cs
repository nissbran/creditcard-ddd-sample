using System;
using System.Threading.Tasks;
using Bank.Cards.Application.Accounts.Commands;
using Bank.Cards.Application.CreditCards.Commands;
using Bank.Cards.Domain.Account;
using Bank.Cards.Domain.Account.Enumerations;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Card;
using Bank.Cards.Domain.Card.Repositories;
using Bank.Cards.Domain.Card.Services;

namespace Bank.Cards.Application.CreditCards.CommandHandlers
{
    public class CreateCardForAccountCommandHandler : ICommandHandler<CreateCardForAccountCommand>
    {
        private readonly ICreditCardRootRepository _creditCardRootRepository;
        private readonly IAccountViewRepository _accountViewRepository;
        private readonly CardNumberGeneratorService _cardNumberGeneratorService;

        public CreateCardForAccountCommandHandler(
            ICreditCardRootRepository creditCardRootRepository,
            IAccountViewRepository accountViewRepository, 
            CardNumberGeneratorService cardNumberGeneratorService)
        {
            _creditCardRootRepository = creditCardRootRepository;
            _accountViewRepository = accountViewRepository;
            _cardNumberGeneratorService = cardNumberGeneratorService;
        }

        public async Task<CommandExecutionResult> Handle(CreateCardForAccountCommand command)
        {
            var account = await _accountViewRepository.GetAccountStatus(command.AccountId);
            
            if (account == null)
                return CommandExecutionResult.NotFound;

            var cardNumber = _cardNumberGeneratorService.GenerateCardNumber();        

            var newCard = new CreditCard(command.CardId, command.AccountId, cardNumber, DateTimeOffset.UtcNow.AddYears(5));

            await _creditCardRootRepository.SaveCard(newCard);
            
            return CommandExecutionResult.Ok;
        }
    }
}