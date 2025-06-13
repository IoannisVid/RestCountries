namespace RestCountries.Application.Dtos
{
    public class CountryName
    {
        [JsonPropertyName("common")]
        public string Common { get; set; }
    }
}
