namespace RestCountries.Tests
{
    public class SecondHighestServiceTests
    {
        private readonly SecondHighestService _service;

        public SecondHighestServiceTests()
        {
            _service = new SecondHighestService();
        }

        [Fact]
        public async Task GetSecondHighest_ValidInput_ReturnsSecondHighest()
        {
            var input = new List<int> { 2, 5, 8, 1 };

            var result = await _service.GetSecondHighest(input);

            Assert.Equal(5, result);
        }

        [Fact]
        public async Task GetSecondHighest_ValidInput_MultipleSameNumbers_ReturnsSecondHighest()
        {
            var input = new List<int> { 2, 2, 5, 5 };

            var result = await _service.GetSecondHighest(input);

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetSecondHighest_OnlySameNumber_ThrowsBadRequestException()
        {
            var input = new List<int> { 5, 5, 5 };

            Func<Task> act = async () => await _service.GetSecondHighest(input);

            await act.Should().ThrowAsync<BadRequestException>()
                .WithMessage("There is only one unique number");
        }

        [Fact]
        public async Task GetSecondHighest_OnlyOneNumber_ThrowsBadRequestException()
        {
            var input = new List<int> { 5 };

            Func<Task> act = async () => await _service.GetSecondHighest(input);

            await act.Should().ThrowAsync<BadRequestException>()
                .WithMessage("There is only one unique number");
        }

        [Fact]
        public async Task GetSecondHighest_EmptyList_ThrowsNotFoundException()
        {
            var input = new List<int>();

            Func<Task> act = async () => await _service.GetSecondHighest(input);

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Data array is empty");
        }
    }
}
