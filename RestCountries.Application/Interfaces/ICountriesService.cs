namespace RestCountries.Core.Interfaces
{
    public interface ICountriesService
    {
        Task<IReadOnlyCollection<CountryDto>> GetCountries();
    }
}
