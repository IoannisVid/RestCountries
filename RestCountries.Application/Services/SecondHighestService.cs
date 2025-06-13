namespace RestCountries.Application.Services
{
    public class SecondHighestService : ISecondHighestService
    {
        public SecondHighestService() { }

        public Task<int> GetSecondHighest(IEnumerable<int> reqData)
        {
            if (reqData.Any())
                throw new Exception("Data array is empty");
            var dat = reqData.OrderByDescending(x => x).Distinct().ToList();
            if (dat.Count < 2)
                throw new Exception("There is only one number");
            return Task.FromResult(dat[1]);
        }
    }
}
