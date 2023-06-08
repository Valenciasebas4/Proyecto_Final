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
                _context.Trainings.Add(new Training { Name = "Brazos", Description = "Esta clase se enfoca en ejercitar los músculos de los brazos, incluyendo los bíceps, tríceps y antebrazos. Se utilizan diferentes movimientos y técnicas como flexiones de brazos, levantamiento de pesas y ejercicios con bandas elásticas para fortalecer y tonificar los músculos de los brazos.", CreatedDate = DateTime.Now });
                _context.Trainings.Add(new Training { Name = "Pilates Power", Description = "Fortalece tu cuerpo desde adentro hacia afuera en esta clase de Pilates centrada en el control y la resistencia muscular. Con una serie de ejercicios que se enfocan en el núcleo, la postura y la alineación corporal, mejorarás tu fuerza, flexibilidad y tonificación muscular. A través de movimientos precisos y fluidos, trabajarás en el fortalecimiento de los músculos profundos y en la mejora de la estabilidad y el equilibrio. Esta clase es adecuada para todas las edades y niveles de condición física, y te ayudará a desarrollar una base sólida para un cuerpo fuerte y saludable.", CreatedDate = DateTime.Now });
                _context.Trainings.Add(new Training { Name = "Danza Latina", Description = "Sumérgete en el ritmo y la pasión de la música latina en esta divertida clase de baile. Desde la salsa y la bachata hasta el merengue y el reguetón, aprenderás los pasos y movimientos característicos de estos estilos de baile. No importa si eres principiante o tienes experiencia, esta clase te brindará una combinación de ejercicio aeróbico, coordinación y diversión. Bailar es una excelente manera de quemar calorías, mejorar el equilibrio y liberar endorfinas. ¡Ven a mover tu cuerpo y contagiarte con la energía de la música latina en cada sesión!", CreatedDate = DateTime.Now });
                _context.Trainings.Add(new Training { Name = "BoxFit: Golpes y Quema", Description = "Prepárate para liberar tu estrés y mejorar tu estado físico en esta clase de BoxFit de alta intensidad. Aprenderás técnicas de boxeo básicas, como golpes, combinaciones y movimientos de defensa, mientras trabajas en tu resistencia cardiovascular y fuerza muscular. Con la ayuda de sacos de boxeo y otros equipos especializados, esta clase te permitirá quemar calorías, tonificar tus músculos y mejorar tu coordinación. ¡Ven listo/a para sudar, golpear con fuerza y liberar tu lado más enérgico en esta clase llena de acción!", CreatedDate = DateTime.Now });
                _context.Trainings.Add(new Training { Name = "Yoga Energético", Description = "Únete a esta clase de yoga dinámico diseñada para aumentar tu energía y vitalidad. A través de una secuencia fluida de posturas que se sincronizan con la respiración, trabajarás en tu fuerza, flexibilidad y equilibrio físico, al mismo tiempo que calmarás tu mente y reducirás el estrés. Esta clase es ideal para aquellos que buscan una combinación perfecta entre ejercicio físico y relajación mental. Ya seas principiante o avanzado/a en yoga, esta clase te ayudará a revitalizarte y renovarte cada semana.", CreatedDate = DateTime.Now });
                _context.Trainings.Add(new Training { Name = "Full Body Blast", Description = "Esta clase intensiva de una hora de duración combina ejercicios de cardio y entrenamiento de fuerza para brindarte un entrenamiento completo de todo el cuerpo. Desde circuitos de alta intensidad hasta ejercicios de levantamiento de pesas, esta clase te desafiará y te ayudará a quemar calorías, tonificar tus músculos y mejorar tu resistencia cardiovascular. ¡Prepárate para sudar y sentirte poderoso/a en esta clase que te dejará totalmente agotado/a y satisfecho/a!\"", CreatedDate = DateTime.Now });



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
