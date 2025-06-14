using System.Linq.Expressions;

namespace RestCountries.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext DbContext { get; set; }
        public Repository(AppDbContext dbContext) => DbContext = dbContext;
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbContext.Set<T>();
            if (includes != null)
            {
                foreach (var tb in includes)
                {
                    query = query.Include(tb);
                }
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id) => await DbContext.Set<T>().FindAsync(id);
        public void Create(T entity) => DbContext.Set<T>().Add(entity);
        public void Delete(T entity) => DbContext.Set<T>().Remove(entity);
    }
}
