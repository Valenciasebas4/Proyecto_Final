using Microsoft.EntityFrameworkCore;
using Proyecto_Final.DAL.Entities;

namespace Proyecto_Final.DAL
{
    public class DataBaseContext :DbContext
    {
        /*Constructor*/
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) 
        {    
        }

        /*Mapeando la entidad para la Tabla*/
        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }

        /*Indicies para las tablas*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
            /* Se usa para validar que el nombre sea Unico*/
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        }
    }
}
