namespace RestCountries.Core.Settings
{
    public sealed class HttpClientSettings
    {
        public int RetryCount { get; set; }
        public int SleepDuration { get; set; }
    }
}
