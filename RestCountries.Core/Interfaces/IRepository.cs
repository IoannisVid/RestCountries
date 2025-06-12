namespace RestCountries.Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void Create(T entity);
        void Delete(T entity);
    }
}
