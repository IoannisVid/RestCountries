namespace RestCountries.Core.Interfaces
{
    public interface ISecondHighestService
    {
        Task<int> GetSecondHighest(IEnumerable<int> reqData);
    }
}
