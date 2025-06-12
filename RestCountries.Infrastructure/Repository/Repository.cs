namespace RestCountries.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext DbContext { get; set; }
        public Repository(AppDbContext dbContext) => DbContext = dbContext;
        public async Task<IEnumerable<T>> GetAllAsync() => await DbContext.Set<T>().ToListAsync();
        public async Task<T> GetByIdAsync(int id) => await DbContext.Set<T>().FindAsync(id);
        public void Create(T entity) => DbContext.Set<T>().Add(entity);
        public void Delete(T entity) => DbContext.Set<T>().Remove(entity);
    }
}
