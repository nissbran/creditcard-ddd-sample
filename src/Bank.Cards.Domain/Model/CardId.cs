using System;

namespace Bank.Cards.Domain.Model
{
    public struct CardId : IEquatable<CardId>
    {
        private readonly Guid _value;

        private CardId(Guid value)
        {
            _value = value;
        }

        public static CardId Empty => new CardId(Guid.Empty);
        public static CardId NewId() => new CardId(Guid.NewGuid());

        public static implicit operator Guid(CardId id) => id._value;
        public static implicit operator string(CardId id) => id._value.ToString();

        public static CardId Parse(string accountId)
        {
            return new CardId(Guid.Parse(accountId));
        }

        public override string ToString() => _value.ToString();

        public bool Equals(CardId other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj is CardId id && Equals(id);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(CardId left, CardId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CardId left, CardId right)
        {
            return !left.Equals(right);
        }
    }
}