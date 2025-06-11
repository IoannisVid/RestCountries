namespace RestCountries.Core.Entities
{
    public class Border
    {
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Country Code is required")]
        public string BorderCode { get; set; }

        public Country Country { get; set; } = null!;
    }
}
