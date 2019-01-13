namespace Bank.Cards.Domain.Card.ValueTypes
{
    public sealed class CardNumber
    {
        public string Pan { get; }

        public CardNumber(string pan)
        {
            Pan = pan;
        }
    }
}