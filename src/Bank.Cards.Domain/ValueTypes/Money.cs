using Bank.Cards.Domain.Enumerations;

namespace Bank.Cards.Domain.ValueTypes
{
    public class Money
    {
        public decimal Value { get; }
        
        public Currency Currency { get; }
        
        public Money(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }
        
        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }
    }
}