using AksDemoApp.Models;
using AksDemoApp.Models.Category;
using AksDemoApp.Models.Products;
using AksDemoApp.Models.SubCategory;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AksDemoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepo categoryRepo;
        private readonly ISubCategoryRepo subCategoryRepo;
        public HomeController(ILogger<HomeController> logger, ICategoryRepo categoryRepo, ISubCategoryRepo subCategoryRepo)
        {
            _logger = logger;
            
            this.categoryRepo=categoryRepo;
            this.subCategoryRepo=subCategoryRepo;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            //var model = categoryRepo.GetCategory();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Categories categories)
        {
            if(ModelState.IsValid)
            {
                var catState = categories.CategoryStatus;
                if (catState != "true")
                {
                    categories.CategoryStatus = "InActive";
                }
                else
                {
                    categories.CategoryStatus = "Active";
                }
                await categoryRepo.AddCategory(categories);
                return new JsonResult(new { success = true });
            }
            return View();
        }
        public async Task<JsonResult> GetAllCategory()
        {
            var data = categoryRepo.GetCategory();
            return Json(data);
        }

        public async Task<JsonResult> EditCategory(int id)
        {
            var data= await categoryRepo.GetCategory(id);
            return new JsonResult(data);
        }

        public async Task<JsonResult> UpdateCategory(Categories categories)
        {
            var catState =  categories.CategoryStatus;
            if (catState != "true" && catState=="false")
            {
                categories.CategoryStatus = "InActive";
            }
            else
            {
                categories.CategoryStatus = "Active";
            }

            var data= categoryRepo.UpdateCategory(categories);

            return new JsonResult(new { success = true });

        }

        public async Task<JsonResult> DeleteCategory(int id)
        {
            var subcat = await categoryRepo.GetSubCategory(id);
            var cat = await categoryRepo.GetCategory(id);
            if (subcat == null && cat != null)
            {

                await categoryRepo.DeleteCategory(id);
                return new JsonResult(new { success = true });
            }
            else
            {
                return new JsonResult(new { success = false });
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}