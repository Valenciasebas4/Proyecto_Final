using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.DAL.Entities;

namespace Proyecto_Final.DAL
{
    public class DataBaseContext : IdentityDbContext<User>
    {
        /*Constructor*/
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) 
        {    
        }

        /*Mapeando la entidad para la Tabla*/
        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<TemporalSale> TemporalSales { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingUser> TrainingsUser { get; set; }


        /*Indicies para las tablas*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
            /* Se usa para validar que el nombre sea Unico*/
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("Name", "CountryId").IsUnique(); // Para estos casos, debo crear un índice Compuesto
            modelBuilder.Entity<City>().HasIndex("Name", "StateId").IsUnique(); // Para estos casos, debo crear un índice Compuesto
            modelBuilder.Entity<Product>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<ProductCategory>().HasIndex("ProductId", "CategoryId").IsUnique();
            modelBuilder.Entity<Training>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<TrainingUser>().HasIndex("User", "Training").IsUnique();



        }
    }
}
