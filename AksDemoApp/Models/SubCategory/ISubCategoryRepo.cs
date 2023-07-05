using AksDemoApp.Models.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;

namespace AksDemoApp.Models.SubCategory
{
    public interface ISubCategoryRepo
    {
        
        public List<SubCategories> GetSubCategories();
        public Task<SubCategories> AddSubCategory(SubCategories subCategories);
        public Task<SubCategories> UpdateSubCategory(SubCategories subCategories);
        public Task<SubCategories> GetSubCategory(int id);
        public Task<SubCategories> DeleteSubCategory(int id);
        public Task<IEnumerable<SelectListItem>> Categorylist();
        public Task <Categories> CategoryName(int id);
        public Task<IEnumerable<SelectListItem>> subCatList(int id);
    }
}
