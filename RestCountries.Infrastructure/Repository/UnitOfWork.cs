namespace RestCountries.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IRepository<Country> _countries;
        public IRepository<Country> Countries
        {
            get
            {
                if (_countries == null)
                {
                    _countries = new Repository<Country>(_dbContext);
                }
                return _countries;
            }
        }
        public UnitOfWork(AppDbContext DBContext)
        {
            _dbContext = DBContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
