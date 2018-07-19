using System;

namespace Bank.Cards.Domain.Account.ValueTypes
{
    public struct AccountId : IEquatable<AccountId>
    {
        private readonly Guid _value;

        private AccountId(Guid value) 
        {
            _value = value;
        }

        public static AccountId Empty => new AccountId(Guid.Empty);
        public static AccountId NewId() => new AccountId(Guid.NewGuid());

        public static implicit operator Guid(AccountId id) => id._value;
        public static implicit operator string(AccountId id) => id._value.ToString();

        public static AccountId Parse(string accountId)
        {
            return new AccountId(Guid.Parse(accountId));
        }
        
        public override string ToString() => _value.ToString();
        
        public bool Equals(AccountId other) 
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj) 
        {
            if (obj is null) return false;
            return obj is AccountId id && Equals(id);
        }

        public override int GetHashCode() 
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(AccountId left, AccountId right) 
        {
            return left.Equals(right);
        }

        public static bool operator !=(AccountId left, AccountId right) 
        {
            return !left.Equals(right);
        }
    }
}