using CPM.UI.Application.Interface;
using CPM.UI.Application.Service;
using CPM.UI.Domain.Model;
//using CPM.UI.Inftrastucture.SendPassword;
using CPM.UI.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace CPM.UI.Controllers
{  

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegisterServices _registerServices;
        private readonly ILoginServices _loginServices;

        public HomeController(ILogger<HomeController> logger, IRegisterServices registerServices, ILoginServices loginServices)
        {
            _logger = logger;
            _registerServices = registerServices;
            _loginServices = loginServices;
        }
        public IActionResult Index()    
        {
            return View();
        }
        public static class PasswordGenerator
        {
            public static string GenerateRandomPassword(int length = 8)
            {
                const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                Random random = new Random();
                return new string(Enumerable.Repeat(validChars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                //string password = PasswordGenerator.GenerateRandomPassword();
                //// string subject = "Password";
                //string body = password;
                //await _emailService.SendEmail(model.Email, body);


                var User = new RegisterDto
                {
                    ClinicName = model.ClinicName,
                    DoctorName = model.DoctorName,
                    SkillId = model.SkillId,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                };
                 await _registerServices.Register(User);
                
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tokenstring = await _loginServices.Login(model);
                if(!string.IsNullOrEmpty(tokenstring))
                {
                    Response.Cookies.Append("AuthToken", tokenstring);
                }
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public IActionResult Dashboard()
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
