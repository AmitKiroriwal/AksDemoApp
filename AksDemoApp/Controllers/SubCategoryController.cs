using AksDemoApp.Models.Category;
using AksDemoApp.Models;
using AksDemoApp.Models.SubCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AksDemoApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AksDemoApp.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryRepo subCategoryRepo;

        public SubCategoryController(ISubCategoryRepo subCategoryRepo)
        {
            this.subCategoryRepo = subCategoryRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddSubCategory()
        {
            ViewBag.Category= await subCategoryRepo.Categorylist();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(SubCategories categories)
        {
                var data = await subCategoryRepo.CategoryName(categories.CategoryId);
                categories.CategoryName = data.CategoryName;

                var catState = categories.SubCategoryStatus;
                if(catState!="true")
                {
                    categories.SubCategoryStatus="InActive";
                }
                else
                {
                    categories.SubCategoryStatus = "Active";
                }

                await subCategoryRepo.AddSubCategory(categories);
                return new JsonResult(new { success = true });
            
                   //return View();
        }
        public async Task<JsonResult> GetAllSubCategory()
        {
            var data = subCategoryRepo.GetSubCategories();
            
            return Json(data);
        }

        public async Task<JsonResult> EditCategory(int id)
        {
            var data = await subCategoryRepo.GetSubCategory(id);
            return new JsonResult(data);
        }

        public async Task <JsonResult> UpdateCategory(SubCategories categories)
        {
            var cat = await subCategoryRepo.CategoryName(categories.CategoryId);
            categories.CategoryName = cat.CategoryName;

            var catState = categories.SubCategoryStatus;
            if (catState != "true")
            {
                categories.SubCategoryStatus = "InActive";
            }
            else
            {
                categories.SubCategoryStatus = "Active";
            }

            var data = await subCategoryRepo.UpdateSubCategory(categories);

            return new JsonResult(new { success = true });
        }

        public async Task<JsonResult> DeleteCategory(int id)
        {
           await subCategoryRepo.DeleteSubCategory(id);

            return new JsonResult(new { success = true });
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
