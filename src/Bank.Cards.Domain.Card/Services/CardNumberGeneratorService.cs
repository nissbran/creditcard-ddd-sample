using System;
using Bank.Cards.Domain.Card.ValueTypes;

namespace Bank.Cards.Domain.Card.Services
{
    public class CardNumberGeneratorService
    {
        private const string Prefix = "521934";

        public CardNumber GenerateCardNumber()
        {
            return new CardNumber(Prefix + new Random().Next(10, 99) + new Random().Next(1000, 9999) + new Random().Next(1000, 9999));
        }
    }
}