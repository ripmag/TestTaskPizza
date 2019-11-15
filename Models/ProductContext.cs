using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace TestTaskPizza.Models
{
    public partial class ProductContext : DbContext
    {
        public ProductContext()
            : base("ProductContext")
        {
        }

        public  DbSet<Client> Clients { get; set; }
        public  DbSet<Product> Products { get; set; }
        public  DbSet<Order> Orders { get; set; }
        public  DbSet<Dish> Dishes { get; set; }
        public  DbSet<Recipe> Recipes { get; set; }
        public  DbSet<Group> Groups { get; set; }
        public  DbSet<DishSelect> DishSelects { get; set; }
        public  DbSet<OrderToProvisioner> OrderToProvisioner { get; set; }
        public  DbSet<ProductArrival> ProductArrival { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           /* base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Group>()
                .HasRequired(a => a.Dish)
                .WithRequiredPrincipal();*/
        }

        // public ProductContext(DbContextTransaction<ProductContext> options) : base (options)           {}

    }

    public class ProductDbInitializer : DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext db)
        {             
            base.Seed(db);
        }
    }
}