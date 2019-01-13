using System;
using System.Threading.Tasks;
using Bank.Cards.Domain.Account;
using Bank.Cards.Domain.Account.Repositories;
using Bank.Cards.Domain.Card;
using Bank.Cards.Domain.Card.Commands;
using Bank.Cards.Domain.Card.Repositories;
using Bank.Cards.Domain.Card.Services;
using Bank.Cards.Domain.Model;

namespace Bank.Cards.Application.CreditCards.Handlers
{
    public class CreateCardForAccountHandler : CommandHandler<CreateCreditCardForAccount>
    {
        private readonly ICreditCardRootRepository _creditCardRootRepository;
        private readonly IAccountViewRepository _accountViewRepository;
        private readonly CardNumberGenerator _cardNumberGenerator;

        public CreateCardForAccountHandler(
            ICreditCardRootRepository creditCardRootRepository,
            IAccountViewRepository accountViewRepository, 
            CardNumberGenerator cardNumberGenerator)
        {
            _creditCardRootRepository = creditCardRootRepository;
            _accountViewRepository = accountViewRepository;
            _cardNumberGenerator = cardNumberGenerator;
        }

        public override async Task<CommandExecutionResult> Handle(CreateCreditCardForAccount command)
        {
            var account = await _accountViewRepository.GetAccountStatus(command.AccountId);
            
            if (account == null)
                return NotFound();

            if (account.Status == AccountStatus.Terminated ||
                account.Status == AccountStatus.Disabled)
                return ValidationError("Account is not ok");

            var cardNumber = _cardNumberGenerator.GenerateCardNumber();

            var newCard = new CreditCard(command.NewCardId, command.AccountId, cardNumber, DateTimeOffset.UtcNow.AddYears(5));

            await _creditCardRootRepository.SaveCard(newCard);
            
            return Ok();
        }
    }
}