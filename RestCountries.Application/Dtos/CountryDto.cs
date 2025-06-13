namespace RestCountries.Application.Dtos
{
    public class CountryDto
    {
        [JsonPropertyName("name")]
        public string Country { get; set; }

        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("borders")]
        public List<string> Borders { get; set; }

    }
}
