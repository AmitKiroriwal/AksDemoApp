using AksDemoApp.Data;
using AksDemoApp.Migrations;
using AksDemoApp.Models.Category;
using AksDemoApp.Models.Products;
using AksDemoApp.Models.SubCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace AksDemoApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ISubCategoryRepo subCategoryRepo;
        private readonly IWebHostEnvironment env;
        private readonly IProductRepo productRepo;
        public ProductController(ISubCategoryRepo subCategoryRepo, IWebHostEnvironment env, IProductRepo productRepo)
        {
            this.subCategoryRepo = subCategoryRepo;
            this.env = env;
            this.productRepo = productRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Category = await subCategoryRepo.Categorylist();
            return View();
        }

        public async Task<IActionResult> Products()
        {
            ViewBag.Category = await subCategoryRepo.Categorylist();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel products)
        {
            if (ModelState.IsValid)
            {
                string uniqeFileName = null;
                string uniqeFile = null;
                string status = null;
                var x = Convert.ToInt32(products.Category);
                var data = await  productRepo.CategoryName(x);
                products.Categoryname = data.CategoryName;
                if (products.PhotoPath != null)
                {
                    string upload = Path.Combine(env.WebRootPath, "images");
                    uniqeFileName = Guid.NewGuid().ToString() + "-" + products.PhotoPath.FileName;
                    string photopath = Path.Combine(upload, uniqeFileName);
                    products.PhotoPath.CopyTo(new FileStream(photopath, FileMode.Create));
                }
                if (products.PdfPath != null && products.PdfPath.Count > 0)
                {
                    foreach (IFormFile pdf in products.PdfPath)
                    {
                        string upload = Path.Combine(env.WebRootPath, "File");
                        uniqeFile = Guid.NewGuid().ToString() + "_" + pdf.FileName;
                        string pdfpath = Path.Combine(upload, uniqeFile);
                        pdf.CopyTo(new FileStream(pdfpath, FileMode.Create));
                    }
                }
                if (products.Status == "true")
                {
                    status = "Active";
                }
                else
                {
                    status = "InActive";
                }
                Products p = new Products()
                { 
                    PdfHeading=products.PdfHeading,
                    Categoryname = products.Categoryname,
                    Category = products.Category,
                    SubCategory = products.SubCategory,
                    ProductName = products.ProductName,
                    PhotoPath = uniqeFileName,
                    ShortDescription = products.ShortDescription,
                    FullDescription = products.FullDescription,
                    Features = products.Features,
                    PdfPath = uniqeFile,
                    Status = status

                };
                await productRepo.AddProduct(p);
                return new JsonResult(new { success = true });
            }
            return View();
        }

        public async Task <JsonResult> UpdateProduct(ProductViewModel products)
        { 
            string uniqeFileName = null;
            string uniqeFile = null;
            string status = null;
            
            var pro = await productRepo.GetProductById(products.Id);
                if (pro != null)
                {
                var x = Convert.ToInt32(products.Category);
                var data = await productRepo.CategoryName(x);
                products.Categoryname =  data.CategoryName;
                pro.PdfHeading = products.PdfHeading;
                    pro.Categoryname = products.Categoryname;
                    pro.Category = products.Category;
                    pro.SubCategory = products.SubCategory;
                    pro.ProductName = products.ProductName;
                    pro.ShortDescription = products.ShortDescription;
                    pro.FullDescription = products.FullDescription;
                    pro.Features = products.Features;
                    if (products.PhotoPath != null)
                    {
                        string upload = Path.Combine(env.WebRootPath, "images");
                        uniqeFileName = Guid.NewGuid().ToString() + "-" + products.PhotoPath.FileName;
                        string photopath = Path.Combine(upload, uniqeFileName);
                        products.PhotoPath.CopyTo(new FileStream(photopath, FileMode.Create));
                        pro.PhotoPath = uniqeFileName;
                    }
                    else
                    {
                        pro.PhotoPath = pro.PhotoPath;
                    }
                    if (products.PdfPath != null && products.PdfPath.Count > 0)
                    {
                        foreach (IFormFile pdf in products.PdfPath)
                        {
                            string upload = Path.Combine(env.WebRootPath, "File");
                            uniqeFile = Guid.NewGuid().ToString() + "_" + pdf.FileName;
                            string pdfpath = Path.Combine(upload, uniqeFile);
                            pdf.CopyTo(new FileStream(pdfpath, FileMode.Create));
                        }
                        pro.PdfPath = uniqeFile;
                    }
                    else
                    {
                        pro.PdfPath = pro.PdfPath;
                    }

                    if (products.Status == "true")
                    {
                        pro.Status = "Active";
                    }
                    else
                    {
                        pro.Status = "Active";
                    }
                }
                await productRepo.UpdateProduct(pro);
        
        
            return new JsonResult(new { success = true });
        }
        public async Task<JsonResult> EditProduct(int id)
        {
            var data = await productRepo.GetProductById(id);
            ViewBag.Category = await subCategoryRepo.Categorylist();
           
            return new JsonResult(data);
        }

        public async Task<JsonResult> SubCategories(int id)
        {
            var data = await subCategoryRepo.subCatList(id);
            return new JsonResult(data);

        }
        public async Task<JsonResult> GetAllProducts()
        {
            var data = productRepo.GetProducts();

            return Json(data);
        }



        public async Task<JsonResult> DeleteProduct(int id)
        {
           await productRepo.DeleteProduct(id);

            return new JsonResult(new { success = true });
        }
        public async Task<IActionResult> FetchsubCat(int id)
        {
            var state = await subCategoryRepo.subCatList(id);
            return Json(state);
        }
    }
}
