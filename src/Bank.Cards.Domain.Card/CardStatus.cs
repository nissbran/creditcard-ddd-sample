namespace Bank.Cards.Domain.Card
{
    internal enum CardStatus
    {
        Created = 0,
        Active = 1,
        Blocked = 2,
        Terminated = 99
    }
}