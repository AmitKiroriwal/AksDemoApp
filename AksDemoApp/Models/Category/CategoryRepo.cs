using AksDemoApp.Data;
using AksDemoApp.Models.SubCategory;
using Microsoft.EntityFrameworkCore;

namespace AksDemoApp.Models.Category
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext appDbContext;

        public CategoryRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Categories> AddCategory(Categories category)
        {
           appDbContext.Categories.Add(category);
          await appDbContext.SaveChangesAsync();
            return category;
        }

        public async Task<SubCategories> GetSubCategory(int id)
        {
            SubCategories subCategories = await appDbContext.SubCategories.FirstOrDefaultAsync(x => x.CategoryId == id);
            return subCategories;
        }
        public async Task<Categories> DeleteCategory(int id)
        {
            Categories categories = appDbContext.Categories.Find(id);
            //var subcat = appDbContext.SubCategories.FirstOrDefault(x=>x.CategoryId == id);

            //if (categories != null && subcat == null)
            //{
                appDbContext.Categories.Remove(categories);
                await appDbContext.SaveChangesAsync();
                return categories;
            //}
            //return null;
            

        }

        public List<Categories> GetCategory()
        {
            var cat= appDbContext.Categories.ToList();
            return cat;
        }

        public async Task<Categories> GetCategory(int id)
        {
            var data= await appDbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            return data;
        }

        public async Task<Categories> UpdateCategory(Categories category)
        {
            Categories categories = appDbContext.Categories.Find(category.CategoryId);
            if(categories!=null)
            {
                categories.CategoryName=category.CategoryName;
                categories.CategoryStatus=category.CategoryStatus;

            }
            await appDbContext.SaveChangesAsync();
            return categories;
        }
    }
}
