using AksDemoApp.Models;
using AksDemoApp.Models.Category;
using AksDemoApp.Models.Products;
using AksDemoApp.Models.SubCategory;
using Microsoft.EntityFrameworkCore;

namespace AksDemoApp.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<SubCategories> SubCategories { get; set; }
        public DbSet<Products> Products { get; set; }

    }
}
