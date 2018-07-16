using Bank.Cards.Domain.Enumerations;

namespace Bank.Cards.Domain.ValueTypes
{
    public class Money
    {
        public decimal Value { get; }
        
        public decimal Vat { get; }

        public Currency Currency { get; }
        
        public Money(decimal value, decimal vat, Currency currency)
        {
            Value = value;
            Vat = vat;
            Currency = currency;
        }
    }
}