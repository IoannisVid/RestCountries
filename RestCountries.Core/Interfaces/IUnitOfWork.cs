namespace RestCountries.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Country> Countries { get; }

        Task SaveAsync();
    }
}
