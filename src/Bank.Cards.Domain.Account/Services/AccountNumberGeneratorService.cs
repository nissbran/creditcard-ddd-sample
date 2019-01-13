using System;

namespace Bank.Cards.Domain.Account.Services
{
    public class AccountNumberGeneratorService
    {
        public AccountNumber GenerateAccountNumber()
        {
            var clearing = $"5{new Random().Next(1000, 9999)}";
            var number = $"00{new Random().Next(100000, 999999)}";   
            
            return new AccountNumber($"{clearing}-{number}");
        }
    }
}