using System.Linq.Expressions;

namespace RestCountries.Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
        void Create(T entity);
        void Delete(T entity);
    }
}
