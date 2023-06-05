using Microsoft.EntityFrameworkCore;
using Proyecto_Final.DAL.Entities;
using Proyecto_Final.Enum;
using Proyecto_Final.Helpers;

namespace Proyecto_Final.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IAzureBlobHelper _azureBlobHelper;

        public SeederDb(DataBaseContext context, IUserHelper userHelper, IAzureBlobHelper azureBlobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _azureBlobHelper = azureBlobHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await PopulateCategoriesAsync();
            await PopulateTrainingsAsync();
            await PopulateCountriesStatesCitiesAsync();
            await PopulateRolesAsync();
            await PopulateProductAsync();
            await PopulateUserAsync("Sebastian", "Londoño", "sebas@yopmail.com", "3142393101", "Barbosa", "1035234145", "Sebas.jpg", UserType.Admin);
            await PopulateUserAsync("Jessica", "Gomez", "jess@yopmail.com", "3188955943", "Barbosa", "1035232261", "Otro.png", UserType.User);
            

            await _context.SaveChangesAsync();
        }


        private async Task PopulateCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Pesas", Description = "Mancuernas, barras, discos", CreatedDate = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Máquinas de ejercicio", Description = "Cintas de correr, elípticas, bicicletas estáticas", CreatedDate = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Accesorios de gimnasio", Description = "Cinturones, guantes, bandas elásticas", CreatedDate = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Suplementos", Description = "Proteínas, creatina, quemadores de grasa", CreatedDate = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Ropa deportiva", Description = "Camisetas, pantalones, zapatillas", CreatedDate = DateTime.Now });
            }
        }

        private async Task PopulateTrainingsAsync()
        {
            if (!_context.Trainings.Any())
            {

                _context.Trainings.Add(new Training { Name = "Pierna", Description = "", CreatedDate = DateTime.Now });
                _context.Trainings.Add(new Training { Name = "Abdomen", Description = "", CreatedDate = DateTime.Now });
                _context.Trainings.Add(new Training { Name = "Cardio", Description = "", CreatedDate = DateTime.Now });
            }
        }


        private async Task PopulateCountriesStatesCitiesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(
                    new Country
                    {
                        Name = "Colombia",
                        CreatedDate = DateTime.Now,
                        States = new List<State>()
                        {
                    new State
                    {
                        Name = "Antioquia",
                        CreatedDate = DateTime.Now,
                        Cities = new List<City>()
                        {
                            new City { Name = "Medellín", CreatedDate = DateTime.Now },
                            new City { Name = "Bello", CreatedDate = DateTime.Now },
                            new City { Name = "Itagüí", CreatedDate = DateTime.Now },
                            new City { Name = "Sabaneta", CreatedDate = DateTime.Now },
                            new City { Name = "Envigado", CreatedDate = DateTime.Now },
                            new City { Name = "Copacabana", CreatedDate = DateTime.Now },
                            new City { Name = "Barbosa", CreatedDate = DateTime.Now },
                            new City { Name = "Girardota", CreatedDate = DateTime.Now },
                        }
                    },

                    new State
                    {
                        Name = "Cundinamarca",
                        CreatedDate = DateTime.Now,
                        Cities = new List<City>()
                        {
                            new City { Name = "Bogotá", CreatedDate = DateTime.Now },
                            new City { Name = "Fusagasugá", CreatedDate = DateTime.Now },
                            new City { Name = "Funza", CreatedDate = DateTime.Now },
                            new City { Name = "Sopó", CreatedDate = DateTime.Now },
                            new City { Name = "Chía", CreatedDate = DateTime.Now },
                        }
                    },

                    new State
                    {
                        Name = "Atlántico",
                        CreatedDate = DateTime.Now,
                        Cities = new List<City>()
                        {
                            new City { Name = "Barranquilla", CreatedDate = DateTime.Now },
                            new City { Name = "La Chinita", CreatedDate = DateTime.Now },
                        }
                    },
                        }
                    });
                _context.Countries.Add(
               new Country
               {
                   Name = "Argentina",
                   CreatedDate = DateTime.Now,
                   States = new List<State>()
                   {
                        new State
                        {
                            Name = "Buenos Aires",
                            CreatedDate = DateTime.Now,
                            Cities = new List<City>()
                            {
                                new City { Name = "Avellaneda", CreatedDate= DateTime.Now },
                                new City { Name = "Ezeiza", CreatedDate= DateTime.Now },
                                new City { Name = "La Boca", CreatedDate= DateTime.Now },
                                new City { Name = "Río de la Plata", CreatedDate= DateTime.Now },
                            }
                        },

                        new State
                        {
                            Name = "La Pampa",
                            CreatedDate = DateTime.Now,
                            Cities = new List<City>()
                            {
                                new City { Name = "Santa María", CreatedDate= DateTime.Now },
                                new City { Name = "Obrero", CreatedDate= DateTime.Now },
                                new City { Name = "Rosario", CreatedDate= DateTime.Now }
                            }
                        }
                   }
               });
            }
        }

        private async Task PopulateRolesAsync()
        {
            await _userHelper.AddRoleAsync(UserType.Admin.ToString());
            await _userHelper.AddRoleAsync(UserType.User.ToString());
        }


        private async Task PopulateUserAsync(string firstName, string lastName, string email, string phone, string address, string document, string image, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                Guid imageId = await _azureBlobHelper.UploadAzureBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\users\\{image}", "users");

                user = new User
                {
                    CreatedDate = DateTime.Now,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                    ImageId = imageId
                };

                await _userHelper.AddUserAsync(user, "123456"); //se establece contraseña para el usuario
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());//agrega el usuario con el rol
            }
        }




        // Productos
        
        private async Task PopulateProductAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Balon Azul", 30000M, 5F, new List<string>() { "Accesorios de gimnasio" }, new List<string>() { "balonazul.png" });
                await AddProductAsync("Balon Gris", 30000M, 5F, new List<string>() { "Accesorios de gimnasio" }, new List<string>() { "balon_gris.png" });
                await AddProductAsync("Balon Rosa", 30000M, 5F, new List<string>() { "Accesorios de gimnasio" }, new List<string>() { "balon_rosa.png" });
                await AddProductAsync("Aro", 25000M, 10F, new List<string>() { "Accesorios de gimnasio" }, new List<string>() { "aroazul.png" });
                await AddProductAsync("Barra Metalica Negra", 180000M, 10F, new List<string>() { "Accesorios de gimnasio" }, new List<string>() { "barra_negra.png" });
                await AddProductAsync("Visera Roja", 15000M, 15F, new List<string>() { "Ropa deportiva" }, new List<string>() { "visera_roja.png" });
                await AddProductAsync("Caminadora", 260000M, 5F, new List<string>() { "Máquinas de ejercicio" }, new List<string>() { "caminadora.png" });
                await AddProductAsync("Colchoneta Morada", 25000M, 50F, new List<string>() { "Accesorios de gimnasio" }, new List<string>() { "colchoneta_morada.png" });


            }
        }

        private async Task AddProductAsync(string name, decimal price, float stock, List<string> categories, List<string> images)
        {
            Product product = new()
            {
                Description = null,
                Name = name,
                Price = price,
                Stock = stock,
                ProductCategories = new List<ProductCategory>(),
                ProductImages = new List<ProductImage>()
            };

            foreach (string? category in categories)
            {
                product.ProductCategories.Add(new ProductCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
            }


            foreach (string? image in images)
            {
                Guid imageId = await _azureBlobHelper.UploadAzureBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\products\\{image}", "products");
                product.ProductImages.Add(new ProductImage { ImageId = imageId });
            }

            _context.Products.Add(product);
        }


        














        /*private async Task PopulateCountriesStatesCitiesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(
                new Country
                {
                    Name = "Colombia",
                    CreatedDate = DateTime.Now,
                    States = new List<State>()
                    {
                        new State
                        {
                            Name = "Antioquia",
                            CreatedDate = DateTime.Now,
                            Cities = new List<City>()
                            {
                                new City { Name = "Medellín", CreatedDate= DateTime.Now },
                                new City { Name = "Bello", CreatedDate= DateTime.Now },
                                new City { Name = "Itagüí", CreatedDate= DateTime.Now },
                                new City { Name = "Sabaneta", CreatedDate= DateTime.Now },
                                new City { Name = "Envigado", CreatedDate= DateTime.Now },
                            }
                        },

                        new State
                        {
                            Name = "Cundinamarca",
                            CreatedDate = DateTime.Now,
                            Cities = new List<City>()
                            {
                                new City { Name = "Bogotá", CreatedDate= DateTime.Now },
                                new City { Name = "Fusagasugá", CreatedDate= DateTime.Now },
                                new City { Name = "Funza", CreatedDate= DateTime.Now },
                                new City { Name = "Sopó", CreatedDate= DateTime.Now },
                                new City { Name = "Chía", CreatedDate= DateTime.Now },
                            }
                        },

                        new State
                        {
                            Name = "Atlántico",
                            CreatedDate = DateTime.Now,
                            Cities = new List<City>()
                            {
                                new City { Name = "Barranquilla", CreatedDate= DateTime.Now },
                                new City { Name = "La Chinita", CreatedDate= DateTime.Now },
                            }
                        },
                    }
                });

                _context.Countries.Add(
                new Country
                {
                    Name = "Argentina",
                    CreatedDate = DateTime.Now,
                    States = new List<State>()
                    {
                        new State
                        {
                            Name = "Buenos Aires",
                            CreatedDate = DateTime.Now,
                            Cities = new List<City>()
                            {
                                new City { Name = "Avellaneda", CreatedDate= DateTime.Now },
                                new City { Name = "Ezeiza", CreatedDate= DateTime.Now },
                                new City { Name = "La Boca", CreatedDate= DateTime.Now },
                                new City { Name = "Río de la Plata", CreatedDate= DateTime.Now },
                            }
                        },

                        new State
                        {
                            Name = "La Pampa",
                            CreatedDate = DateTime.Now,
                            Cities = new List<City>()
                            {
                                new City { Name = "Santa María", CreatedDate= DateTime.Now },
                                new City { Name = "Obrero", CreatedDate= DateTime.Now },
                                new City { Name = "Rosario", CreatedDate= DateTime.Now }
                            }
                        }
                    }
                });
            }
        }*/
    }
}
