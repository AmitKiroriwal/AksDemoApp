using AksDemoApp.Data;
using AksDemoApp.Models.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;

namespace AksDemoApp.Models.Products
{
    
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext appDbContext;

        public ProductRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Products> AddProduct(Products product)
        {
            appDbContext.Products.Add(product);
            appDbContext.SaveChanges();
            return product;
        }

        public Task<IEnumerable<SelectListItem>> CategoryList()
        {
            throw new NotImplementedException();
        }

        public async Task <Categories> CategoryName(int id)
        {
            var data = appDbContext.Categories.FirstOrDefault(x => x.CategoryId == id);
            return  data;
        }

        public async Task< Products> DeleteProduct(int id)
        {
            var data = await appDbContext.Products.FindAsync(id);
            if(data != null)
            {
                appDbContext.Products.Remove(data);
                appDbContext.SaveChanges();
            }
            return data;
        }

        public async Task< Products> GetProductById(int id)
        {
            return await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public List<Products> GetProducts()
        {
          return  appDbContext.Products.Where(x => x.Status == "Active").ToList();

        }

        
        public Task<IEnumerable<SelectListItem>> SubCategoryList()
        {
            throw new NotImplementedException();
        }

        public async Task<Products> UpdateProduct(Products product)
        {
            
            var data= appDbContext.Products.Attach(product);
             data.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            appDbContext.SaveChanges();
            return product;
        }
    }
}
