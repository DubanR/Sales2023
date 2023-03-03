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
                _context.Countries.Add(new Country
                {
                    name = "Colombia",
                    States = new List<State>()
                    {
                        new State()
                        {
                            name = "Antioquia",
                            Cities = new List<City>()
                            {
                                new City() { name = "Medellín" },
                                new City() { name = "Itagüí" },
                                new City() { name = "Envigado" },
                                new City() { name = "Bello" },
                                new City() { name = "Rionegro" },
                            }
                        },
                        new State()
                        {
                            name = "Bogotá",
                            Cities = new List<City>()
                            {
                                new City() { name = "Usaquen" },
                                new City() { name = "Champinero" },
                                new City() { name = "Santa fe" },
                                new City() { name = "Useme" },
                                new City() { name = "Bosa" },
                            }
                        },
                    }
                });
                _context.Countries.Add(new Country
                {
                    name = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State()
                        {
                            name = "Florida",
                            Cities = new List<City>()
                            {
                                new City() { name = "Orlando" },
                                new City() { name = "Miami" },
                                new City() { name = "Tampa" },
                                new City() { name = "Fort Lauderdale" },
                                new City() { name = "Key West" },
                            }
                         },
                        new State()
                        {
                            name = "Texas",
                            Cities = new List<City>()
                            {
                                new City() { name = "Houston" },
                                new City() { name = "San Antonio" },
                                new City() { name = "Dallas" },
                                new City() { name = "Austin" },
                                new City() { name = "El Paso" },
                            }
                        },
                    }
                });
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
                _context.Categories.Add(new Category { name = "Niño" });
                _context.Categories.Add(new Category { name = "Niña" });
                _context.Categories.Add(new Category { name = "Jugueteria" });
                _context.Categories.Add(new Category { name = "Deportes" });
                _context.Categories.Add(new Category { name = "Cosmeticos" });
                _context.Categories.Add(new Category { name = "Tecnologia" });
                _context.Categories.Add(new Category { name = "Electrodomesticos" });
                _context.Categories.Add(new Category { name = "Mascotas" });
                _context.Categories.Add(new Category { name = "Papeleria" });

                await _context.SaveChangesAsync();
            }
        }
    }
}
