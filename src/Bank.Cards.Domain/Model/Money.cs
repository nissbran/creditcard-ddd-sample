using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Bank.Cards.Domain.Model
{
    public class Money : IEquatable<Money>
    {
        public decimal Value { get; }
        
        public Currency Currency { get; }

        public bool IsEmpty => Value == 0;

        private Money(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }
        
        public static Money Create(decimal value, Currency currency) => new Money(value, currency);
        
        private static readonly Regex ParseRegex = new Regex(@"^(\-?[0-9\.,]+) ([a-zA-Z]{3})$", RegexOptions.Compiled);
        
        public static Money Parse(string value, IFormatProvider culture)
        {
            var match = ParseRegex.Match(value);
            if (!match.Success) throw new Exception($"Could not parse the string value '{value}' into a Money object.");

            var amountString = match.Groups[1].Value;
            var amount = decimal.Parse(amountString, culture);

            var currencySymbol = match.Groups[2].Value;
            var currency = Currency.Parse(currencySymbol);

            return new Money(amount, currency);
        }
        
        public override string ToString() => $"{Value:N2} {Currency.Code}";

        public string ToFullPrecisionString(IFormatProvider culture) => string.Format(culture, "{0} {1}", Value, Currency.Code);

        public Money Add(Money other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (IsEmpty) return other;
            if (other.IsEmpty) return this;
            if (!Currency.Equals(other.Currency))
                throw new InvalidOperationException("Cannot add different currencies.");
            
            return new Money(Value + other.Value, Currency);
        }

        public Money Subtract(Money other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (IsEmpty) return new Money(-other.Value, other.Currency);
            if (other.IsEmpty) return this;
            if (!Currency.Equals(other.Currency))
                throw new InvalidOperationException("Cannot subtract different currencies.");
            
            return new Money(Value - other.Value, Currency);
        }

        public Money Multiply(Money other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (IsEmpty) return this;
            if (!Currency.Equals(other.Currency))
                throw new InvalidOperationException("Cannot multiply different currencies.");
            
            return new Money(Value*other.Value, Currency);
        }

        public Money DivideBy(Money other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!Currency.Equals(other.Currency))
                throw new InvalidOperationException("Cannot divide different currencies.");
            
            return new Money(Value/other.Value, Currency);
        }

        public static Money operator +(Money a, Money b) => a.Add(b);

        public static Money operator -(Money a, Money b) => a.Subtract(b);

        public static Money operator *(Money a, Money b) => a.Multiply(b);

        public static Money operator /(Money a, Money b) => a.DivideBy(b);

        public static Money operator -(Money a) => new Money(-a.Value, a.Currency);

        public static implicit operator decimal(Money value) => value.Value;

        public static implicit operator Currency(Money value) => value.Currency;

        public static implicit operator string(Money value) => value.ToString();

        public static implicit operator Money(string value) => Parse(value, CultureInfo.InvariantCulture);

        public static Money ToRounded(Money value) => new Money(Math.Round(value.Value, 2), value.Currency);

        public static Money ToRoundedUp(Money value) => new Money(Math.Ceiling(value.Value), value.Currency);

        public static Money ToRoundedDown(Money value) => new Money(Math.Floor(value.Value), value.Currency);

        public bool Equals(Money other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value && Equals(Currency, other.Currency);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Money) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ (Currency != null ? Currency.GetHashCode() : 0);
            }
        }
    }
}