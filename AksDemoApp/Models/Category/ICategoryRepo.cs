using AksDemoApp.Models.SubCategory;

namespace AksDemoApp.Models.Category
{
    public interface ICategoryRepo
    {
        public List<Categories> GetCategory();
        public Task<Categories> AddCategory(Categories category);
        public Task<Categories> UpdateCategory(Categories category);
        public Task< Categories> DeleteCategory(int id);
        public Task< Categories> GetCategory(int id);
        public Task<SubCategories> GetSubCategory(int id);
    }
}
