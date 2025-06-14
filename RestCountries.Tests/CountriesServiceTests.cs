namespace RestCountries.Tests
{
    public class CountriesServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();
        private readonly IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        public CountriesServiceTests() { }

        private CountriesService GetService()
        {
            return new CountriesService(
                 _httpClientFactoryMock.Object,
                 _memoryCache,
                 _unitOfWorkMock.Object,
                 _mapperMock.Object);
        }

        [Fact]
        public async Task GetCountries_CacheHasValues_ReturnsCachedCountries()
        {
            var expected = new List<CountryDto> { new CountryDto { Country = "Fakeland", Capital = "Fakecapital", Borders = new List<string>() { "ABC" } } };
            _memoryCache.Set("Countries", expected);

            var result = await GetService().GetCountries();

            Assert.Equal(expected.Count, result.Count);
        }

        [Fact]
        public async Task GetCountries_CacheEmpty_LoadFromDb_ReturnsMappedCountriesAndCache()
        {
            var dbCountries = new List<Country> { new Country { CountryName = "Fakeland", CountryCapital = "Fakecapital" } };

            var mockRepo = new Mock<IRepository<Country>>();
            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Country, object>>>()))
                .ReturnsAsync(dbCountries);

            _unitOfWorkMock.Setup(u => u.Countries).Returns(mockRepo.Object);

            var mapped = new List<CountryDto> { new CountryDto { Country = "Fakeland", Capital = "Fakecapital", Borders = new List<string>() { "ABC" } } };
            _mapperMock.Setup(m => m.Map<List<CountryDto>>(dbCountries)).Returns(mapped);

            var result = await GetService().GetCountries();

            Assert.Equal("Fakeland", result.First().Country);
            Assert.True(_memoryCache.TryGetValue("Countries", out _));
        }

        [Fact]
        public async Task GetCountries_CacheEmpty_GetFromApi_ReturnsInsertedCountriesAndCache()
        {
            var mockRepo = new Mock<IRepository<Country>>();
            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Country, object>>>()))
                .ReturnsAsync(new List<Country>());

            _unitOfWorkMock.Setup(u => u.Countries).Returns(mockRepo.Object);

            var restCountries = new List<RestCountry>
            {
                new RestCountry
                {
                    Country = new CountryName { Common = "Fakeland" },
                    Capital = new List<string> { "Fakecapital" },
                    Borders = new List<string> { "ABC" }
                }
            };

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(HttpMethod.Get, "https://fakeapi.com/all?fields=name,capital,borders")
                .Respond("application/json", JsonSerializer.Serialize(restCountries));
            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri("https://fakeapi.com");
            _httpClientFactoryMock.Setup(f => f.CreateClient("Client")).Returns(client);

            _mapperMock.Setup(m => m.Map<List<CountryDto>>(It.IsAny<List<Country>>()))
                .Returns(new List<CountryDto> { new CountryDto { Country = "Fakeland", Capital = "Fakecapital", Borders = new List<string>() { "ABC" } } });

            var result = await GetService().GetCountries();

            Assert.Equal("Fakeland", result.First().Country);
            Assert.True(_memoryCache.TryGetValue("Countries", out _));
        }

        [Fact]
        public async Task GetCountries_CacheEmpty_ApiReturnsEmpty_ThrowsNotFoundException()
        {
            var mockRepo = new Mock<IRepository<Country>>();
            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Country, object>>>()))
                .ReturnsAsync(new List<Country>());

            _unitOfWorkMock.Setup(u => u.Countries).Returns(mockRepo.Object);

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(HttpMethod.Get, "https://fakeapi.com/all?fields=name,capital,borders")
                .Respond("application/json", JsonSerializer.Serialize(new List<RestCountry>()));
            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri("https://fakeapi.com");
            _httpClientFactoryMock.Setup(f => f.CreateClient("Client")).Returns(client);

            await Assert.ThrowsAsync<NotFoundException>(() => GetService().GetCountries());
        }
    }
}
