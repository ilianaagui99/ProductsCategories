using Microsoft.EntityFrameworkCore;
namespace ProductsyCategories.Models
{ 
    // the MyContext class representing a session with our MySQL 
    // database allowing us to query for or save data
    public class MyContext : DbContext 
    { 
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> productsT { get; set; }
        public DbSet<Category> categoriesT {get; set;}
        public DbSet<Association> associationsT {get; set;}
    }
}