using System;
using Bank.Cards.Domain.Model;
using Bank.Cards.Infrastructure.Serialization.Converters;
using Newtonsoft.Json.Serialization;

namespace Bank.Cards.Infrastructure.Serialization
{
    public class DomainJsonContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);
            
            if (objectType == typeof(AccountId))
                contract.Converter = new AccountIdConverter();
            if (objectType == typeof(Money))
                contract.Converter = new MoneyConverter();
            if (objectType == typeof(Currency))
                contract.Converter = new CurrencyConverter();
            if (objectType == typeof(Country))
                contract.Converter = new CountryConverter();

            return contract;
        }
    }
}