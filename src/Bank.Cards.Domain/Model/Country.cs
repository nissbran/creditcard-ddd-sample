using System;
using System.Collections.Generic;

namespace Bank.Cards.Domain.Model
{
    public sealed class Country : IEquatable<Country>
    {
        public string Code { get; }
        
        public string Name { get; }
        
        public Currency DefaultCurrency { get; }

        private Country(string code, string name, Currency defaultCurrency)
        {
            Code = code;
            Name = name;
            DefaultCurrency = defaultCurrency;
        }
        
        public static Country Sweden => new Country("SE", "Sweden", Currency.SEK);
        public static Country Finland => new Country("FI", "Finland", Currency.EUR);

        private static readonly IReadOnlyDictionary<string, Country> ValidCountries = new Dictionary<string, Country>
        {
            {Sweden.Code, Sweden},
            {Finland.Code, Finland}
        };
        
        public static Country Parse(string code)
        {
            if (ValidCountries.TryGetValue(code.ToUpperInvariant(), out var country))
                return country;
            
            throw new FormatException($"Country code {code}, is not a valid country");
        }
        
        public bool Equals(Country other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Code, other.Code);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Country) obj);
        }

        public override int GetHashCode()
        {
            return (Code != null ? Code.GetHashCode() : 0);
        }
    }
}