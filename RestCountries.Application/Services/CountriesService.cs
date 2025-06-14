namespace RestCountries.Application.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountriesService> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountriesService(ILogger<CountriesService> logger, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("Client");
            _logger = logger;
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<CountryDto>> GetCountries()
        {
            if (!_memoryCache.TryGetValue("Countries", out List<CountryDto> countries))
            {
                var data = await _unitOfWork.Countries.GetAllAsync(x => x.Borders);
                if (data.Any())
                {
                    countries = _mapper.Map<List<CountryDto>>(data);
                }
                else
                {
                    var restCountries = await GetRestCountries();
                    var dat = await InsertCountries(restCountries);
                    countries = _mapper.Map<List<CountryDto>>(dat);
                }
                AddCountriesInCache(countries);
            }
            return countries.AsReadOnly();
        }

        private async Task<List<RestCountry>> GetRestCountries()
        {
            var response = await _httpClient.GetAsync("all?fields=name,capital,borders");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<List<RestCountry>>(content);
            return res;
        }

        private void AddCountriesInCache(List<CountryDto> countries)
        {
            _memoryCache.Set("Countries", countries, new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(10) });
        }

        private async Task<List<Country>> InsertCountries(List<RestCountry> restCountries)
        {
            List<Country> countries = new List<Country>();
            foreach (var cntry in restCountries)
            {
                var country = new Country
                {
                    CountryName = cntry.Country.Common,
                    CountryCapital = cntry.Capital?.FirstOrDefault() ?? "",
                    Borders = cntry.Borders.Select(code => new Border
                    {
                        BorderCode = code
                    }).ToList()
                };
                countries.Add(country);
                _unitOfWork.Countries.Create(country);
            }
            await _unitOfWork.SaveAsync();
            return countries;
        }
    }
}

