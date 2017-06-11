namespace InstaSharper.Classes
{
    public enum ResponseType
    {
        Unknown = 0,
        LoginRequired = 1,
        CheckPointRequired = 2,
        RequestsLimit = 3,
        SentryBlock = 4,
        OK = 5
    }
}