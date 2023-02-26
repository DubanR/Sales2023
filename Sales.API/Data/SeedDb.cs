using Sales.Shared.Entities;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country { name = "Colombia" });
                _context.Countries.Add(new Country { name = "Chile" });
                _context.Countries.Add(new Country { name = "Mexico" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { name = "Calzado" });
                _context.Categories.Add(new Category { name = "Hombre" });
                _context.Categories.Add(new Category { name = "Mujer" });

                await _context.SaveChangesAsync();
            }
        }
    }
}
