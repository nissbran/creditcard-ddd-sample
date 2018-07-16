namespace Bank.Cards.Domain
{
    public struct EventType
    {
        public EventType(string eventName) : this(eventName, 1)
        {
        }

        public EventType(string eventName, int latestVersion)
        {
            EventName = eventName;
            LatestVersion = latestVersion;
        }

        public string EventName { get; }

        public int LatestVersion { get; }

        public static implicit operator string(EventType type)
        {
            return type.EventName;
        }
    }
}