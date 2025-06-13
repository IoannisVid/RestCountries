namespace RestCountries.Application.Dtos
{
    public sealed class RestCountry
    {
        [JsonPropertyName("name")]
        public CountryName Country { get; set; }

        [JsonPropertyName("capital")]
        public List<string> Capital { get; set; }

        [JsonPropertyName("borders")]
        public List<string> Borders { get; set; }
    }
}

