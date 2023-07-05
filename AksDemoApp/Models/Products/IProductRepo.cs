using AksDemoApp.Models.Category;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AksDemoApp.Models.Products
{
    public interface IProductRepo
    {
        public List<Products> GetProducts();
        public Task<Products> AddProduct(Products product);
        public Task<Products> UpdateProduct(Products product);
        public Task< Products> GetProductById(int id);
        public Task< Products> DeleteProduct(int id);

        public Task<IEnumerable<SelectListItem>> CategoryList();
        public Task<IEnumerable<SelectListItem>> SubCategoryList();
        public Task <Categories> CategoryName(int id);

    }
}
