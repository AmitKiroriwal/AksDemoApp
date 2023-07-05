using AksDemoApp.Data;
using AksDemoApp.Models.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AksDemoApp.Models.SubCategory
{
    public class SubCategoryRepo : ISubCategoryRepo
    {
        private readonly AppDbContext appDbContext;

        public SubCategoryRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<SubCategories> AddSubCategory(SubCategories subCategories)
        {
            appDbContext.SubCategories.Add(subCategories);
           await appDbContext.SaveChangesAsync();
            return subCategories;
        }

        public async Task<SubCategories> DeleteSubCategory(int id)
        {
            SubCategories categories = await appDbContext.SubCategories.FindAsync(id);
            
            if (categories != null)
            {
                appDbContext.SubCategories.Remove(categories);
              await appDbContext.SaveChangesAsync();
            }

            return categories;
        }

        public  List<SubCategories> GetSubCategories()
        {
           return appDbContext.SubCategories.ToList();

        }
        public async Task<Categories> CategoryName(int id)
        {
            var data = await appDbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            return  data;
        }
        public async Task<SubCategories> GetSubCategory(int id)
        {
            return await appDbContext.SubCategories.FirstOrDefaultAsync(x => x.SubCategoryId == id);
        }

        public async Task<SubCategories> UpdateSubCategory(SubCategories subCategories)
        {
            SubCategories categories = await appDbContext.SubCategories.FindAsync(subCategories.SubCategoryId);
            if (categories != null)
            {
                categories.CategoryName=subCategories.CategoryName;
                categories.SubCategoryId = subCategories.SubCategoryId;
                categories.SubCategoryName = subCategories.SubCategoryName;
                categories.SubCategoryStatus = subCategories.SubCategoryStatus;
            }
              await appDbContext.SaveChangesAsync();
            return categories;
        }

        public async Task<IEnumerable<SelectListItem>> Categorylist()
        {
            var data = appDbContext.Categories.Where(x=>x.CategoryStatus=="Active").Select(c => new { Name = c.CategoryName, id = c.CategoryId });
            var res = await data.Select(x => new SelectListItem { Text = x.Name, Value = x.id.ToString() }).ToListAsync();
            return res;
        }
        
        public async Task<IEnumerable<SelectListItem>> subCatList(int id)
        {
            var data = appDbContext.SubCategories.Where(x => x.CategoryId == id && x.SubCategoryStatus=="Active").Select(s => new { Name = s.SubCategoryName, id = s.SubCategoryId });
            var res = await data.Select(x => new SelectListItem { Text = x.Name, Value = x.Name }).ToListAsync();
            return res;
        }
    }
}
