using AksDemoApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AksDemoApp.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly IAdminRepo adminRepo;

        public LoginController(IAdminRepo adminRepo)
        {
            this.adminRepo = adminRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Admin admin)
        {
            var User= adminRepo.GetAdmin(admin);
            if (User != null)
            {
                return RedirectToAction("Dashboard");

            }
            else
            {
                ViewBag.Msg = "Username or Password is Incorrect";
            }
            return View();

        }
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            if(ModelState.IsValid)
            {
                var admin = adminRepo.GetAdminPass(changePassword.oldPassword);
                if(admin!=null)
                {
                    admin.Password=changePassword.NewPassword;
                }
                adminRepo.Update(admin);
                ViewBag.Pass = "Password updated Successfully!";
                
            }
            return View();
        }
        
    }
}
