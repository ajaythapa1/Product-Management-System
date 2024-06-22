using Ajay.PMS.ModelConfiguration;
using Ajay.PMS.Models;
using Microsoft.EntityFrameworkCore;

namespace Ajay.PMS.Data
{
    public class ApplicationDbContext : DbContext
    {
   
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
                
        }

        public DbSet <Category> Category { get; set; }

        public DbSet <Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
