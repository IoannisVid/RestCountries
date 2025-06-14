namespace RestCountries.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Border> Borders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Border>()
               .HasKey(b => new { b.CountryId, b.BorderCode });

            modelBuilder.Entity<Border>()
                .HasOne(b => b.Country)
                .WithMany(c => c.Borders)
                .HasForeignKey(b => b.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
