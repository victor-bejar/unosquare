using Microsoft.EntityFrameworkCore;

using App.Persistence.Model;


namespace App.Persistence.Class
{

    public class UnoSquareContext : DbContext
    {

        public DbSet<Product> Products { get; set; }

        public UnoSquareContext(DbContextOptions<UnoSquareContext> options) : base(options)
        {
        }

        // TODO: Set indexes to search fields
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // business
            modelBuilder.Entity<Product>().
                HasIndex(x => new { x.Name }).IsUnique(true);

        }
 
    }

}