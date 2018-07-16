using System;
using System.Collections.Generic;

namespace Bank.Cards.Domain.Enumerations
{
    public class Currency
    {
        public string Code { get; }
        
        public string Name { get; }
        
        private Currency(string code, string name)
        {
            Code = code;
            Name = name;
        }
        
        public static readonly Currency SEK = new Currency("SEK", "Swedish Krona"); 
        public static readonly Currency EUR = new Currency("EUR", "Euro");

        private static readonly IReadOnlyDictionary<string, Currency> ValidCurrencies = new Dictionary<string, Currency>
        {
            {SEK.Code, SEK},
            {EUR.Code, EUR}
        };
        
        public static Currency Parse(string code)
        {
            if (ValidCurrencies.TryGetValue(code.ToUpperInvariant(), out var currency))
                return currency;
            
            throw new FormatException($"Currency code {code}, is not a valid currency");
        }
    }
}