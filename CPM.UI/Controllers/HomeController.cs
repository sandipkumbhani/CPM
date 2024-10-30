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
        private readonly IEmailSendService _emailSendService;

        public HomeController(ILogger<HomeController> logger, IRegisterServices registerServices, ILoginServices loginServices, IEmailSendService emailSendService)
        {
            _logger = logger;
            _registerServices = registerServices;
            _loginServices = loginServices;
            _emailSendService = emailSendService;
        }
        public IActionResult Index()    
        {
            return View();
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
                _emailSendService.SendEmail(model.Email);
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
