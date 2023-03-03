using Microsoft.EntityFrameworkCore;
using Sales.Shared.Entities;

namespace Sales.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x => x.name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("Countryid", "name").IsUnique();
            modelBuilder.Entity<City>().HasIndex("Stateid", "name").IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.name).IsUnique();
        }

    }
}
