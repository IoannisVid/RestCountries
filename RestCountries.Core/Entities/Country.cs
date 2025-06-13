namespace RestCountries.Core.Entities
{
    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Country Name is required")]
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Capital of Country is required")]
        public string CountryCapital { get; set; }

        public ICollection<Border> Borders { get; set; } = new List<Border>();
    }
}
