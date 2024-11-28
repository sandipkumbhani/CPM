using CPM.UI.Application.Interface;
using CPM.UI.Domain.Model;
//using CPM.UI.Inftrastucture.SendPassword;
using CPM.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using static CPM.UI.Application.Service.EmailSendService;
using System.Configuration;
using System.Data.SqlClient;
namespace CPM.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegisterServices _registerServices;
        private readonly ILoginServices _loginServices;
        private readonly IEmailSendService _emailSendService;
        private readonly ISkillServices _skillServices;
        private readonly IDoctorServices _doctorServices;
        private readonly IClinicServices _clinicServices;
        private readonly GlobalClass _globalClass;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPatientServices _patientServices;
        private readonly IPatientDiagnosisServices _patientDiagnosisServices;
        public HomeController(ILogger<HomeController> logger, IRegisterServices registerServices, ILoginServices loginServices, IEmailSendService emailSendService, ISkillServices skillServices, IDoctorServices doctorServices, IClinicServices clinicServices, GlobalClass globalClass, IPasswordHasher passwordHasher, IPatientServices petientServices, IPatientDiagnosisServices patientDiagnosisServices)
        {
            _logger = logger;
            _registerServices = registerServices;
            _loginServices = loginServices;
            _emailSendService = emailSendService;
            _skillServices = skillServices;
            _doctorServices = doctorServices;
            _clinicServices = clinicServices;
            _globalClass = globalClass;
            _passwordHasher = passwordHasher;
            _patientServices = petientServices;
            _patientDiagnosisServices = patientDiagnosisServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            var skilllist = await _skillServices.GetAllSkill();
            ViewBag.skill = skilllist;
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
                //_emailSendService.SendEmail(model.Email);
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
                if (!string.IsNullOrEmpty(tokenstring))
                {
                    Response.Cookies.Append("AuthToken", tokenstring);
                }
                if (tokenstring != null)
                {
                    return RedirectToAction("Checkpassword_Change");
                }
                return RedirectToAction("Dashboard");
            }
            return View();
        }
        public async Task<IActionResult> Checkpassword_Change()
        {
            if (_globalClass.Token != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_globalClass.Token);
                string Email = jwt.Claims.First(c => c.Type == "email").Value;
                string Doctorid = jwt.Claims.First(c => c.Type == "doctorid").Value;
                var doctor = await _doctorServices.GetById(Convert.ToInt32(Doctorid));
                var clinics = await _clinicServices.GetAll();
                var clinic = clinics.Where(x => x.ClinicId == doctor.ClinicId).FirstOrDefault();
                if (clinic.IsPasswordChange == 0)
                {
                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    return RedirectToAction("Dashboard");
                }
            }
            return RedirectToAction("Login");
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Dashboard(PatientDiagnosisViewModel model)
        {
            if (_globalClass.Token != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_globalClass.Token);
                string Doctorid = jwt.Claims.First(c => c.Type == "doctorid").Value;
                string rolename = jwt.Claims.First(c => c.Type == "rolename").Value;
                var Doctor = await _doctorServices.GetById(Convert.ToInt32(Doctorid));
                var clinicid = Doctor.ClinicId;
                var clinicname = Doctor.ClinicMaster.Name;
                var dignosis = await _patientDiagnosisServices.GetAll();
                var dignocies = dignosis.Where(x => x.Patient.ClinicId == clinicid && x.IsQueue==true);
                if (dignocies != null && dignocies.Count() > 0)
                {
                    model.PatientDiagnosisDto.AddRange(dignocies);
                }
                ViewBag.clinic = clinicname;
                ViewBag.role = rolename;
                return View(model);
            }
            return RedirectToAction("Login");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_globalClass.Token != null)
                {
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_globalClass.Token);
                    string Loginid = jwt.Claims.First(c => c.Type == "loginid").Value;
                    string Email = jwt.Claims.First(c => c.Type == "email").Value;
                    string Roleid = jwt.Claims.First(c => c.Type == "roleid").Value;
                    string Doctorid = jwt.Claims.First(c => c.Type == "doctorid").Value;
                    string Password = jwt.Claims.First(c => c.Type == "password").Value;
                    if (_passwordHasher.VerifyPassword(Password, model.CurrentPassword))
                    {
                        //var clinics = await _clinicServices.GetAll();
                       // var clinic = clinics.Where(x => x.EmailId == Email).FirstOrDefault();
                        var Doctors = await _doctorServices.GetAllDoctor();
                        var doctor = Doctors.Where(X => X.DoctorEmail == Email).FirstOrDefault();
                        var cid = doctor.ClinicId;
                        var clinic = await _clinicServices.GetById(Convert.ToInt32(cid));
                        var user = new LoginDto()
                        {
                            DoctorId = Convert.ToInt32(Doctorid),
                            EmailId = Email,
                            Password = model.NewPassword,
                            RoleId = Convert.ToInt32(Roleid),
                        };
                        await _loginServices.UpdateUser(Convert.ToInt32(Loginid), user);
                            clinic.IsPasswordChange = 1;
                            await _clinicServices.UpdateClinic(clinic.ClinicId, clinic);
                        Response.Cookies.Delete("AuthToken");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public async Task<IActionResult> GetSkills(SkillMasterViewModel model)
        {
            var userlist = await _skillServices.GetAllSkill();
            if (userlist != null && userlist.Count() > 0)
            {
                model.SkillDto.AddRange(userlist);
            }
            return View(model);
        }
        public IActionResult Doctor()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Doctor(DoctorMasterViewModel model)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_globalClass.Token);
            string rolename = jwt.Claims.First(c => c.Type == "rolename").Value;
            string Doctorid = jwt.Claims.First(c => c.Type == "doctorid").Value;
            var Doctor = await _doctorServices.GetById(Convert.ToInt32(Doctorid));
            var clinicname = Doctor.ClinicMaster.Name;
            if (rolename == "Admin")
            {
                var details = await _doctorServices.GetAllDoctor();
                if (details != null && details.Count() > 0)
                {
                    model.DoctorDto.AddRange(details);

                }
            }
            if (rolename == "Doctor" || rolename == "Receptionist")
            {
                var clinicid = Doctor.ClinicId;
                var details = await _doctorServices.GetAllDoctor();
                var detail = details.Where(x => x.ClinicId == clinicid).ToList();
                if (detail != null && detail.Count() > 0)
                {
                    model.DoctorDto.AddRange(detail);
                }
            }
            ViewBag.clinic = clinicname;
            ViewBag.role = rolename;
            return View(model);
        }
        public IActionResult Clinic()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            var detail = await _clinicServices.GetById(id);
            if (detail != null)
            {
                detail.IsApprove = 1;
                var result = await _clinicServices.UpdateClinic(id, detail);
                var Doctors = await _doctorServices.GetAllDoctor();
                var doctor = Doctors.Where(X => X.ClinicId == id).ToList();
                foreach (var user in doctor)
                {
                    var loggeduser = await _loginServices.GetByEmail(user.DoctorEmail);
                    if (loggeduser == null)
                    {
                        var password = PasswordGenerator.GenerateRandomPassword();
                        _emailSendService.SendEmail(user.DoctorEmail, password);
                        var User = new LoginDto()
                        {
                            DoctorId = user.DoctorId,
                            EmailId = user.DoctorEmail,
                            Password = password,
                            RoleId = user.RoleId,
                        };
                        result = await _loginServices.AddUser(User);
                    }
                }
                if (result != null)
                {
                    return Json(new { result = "success" });
                }
                else
                {
                    return Json(new { result = "failure" });
                }
            }
            return null;
        }
        [HttpGet]
        public async Task<IActionResult> Clinic(ClinicMasterViewModel model)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_globalClass.Token);
            string rolename = jwt.Claims.First(c => c.Type == "rolename").Value;
            string Doctorid = jwt.Claims.First(c => c.Type == "doctorid").Value;
            var Doctor = await _doctorServices.GetById(Convert.ToInt32(Doctorid));
           var clinicname = Doctor.ClinicMaster.Name;
            if (rolename == "Admin")
            {
                var details = await _clinicServices.GetAll();
                if (details != null && details.Count() > 0)
                {
                    model.ClinicDto.AddRange(details);
                }
            }
            if (rolename == "Doctor" || rolename == "Receptionist")
            {
                var clinicid = Doctor.ClinicId;
                var details = await _clinicServices.GetAll();
                var detail = details.Where(x => x.ClinicId == clinicid).ToList();
                if (detail != null && detail.Count() > 0)
                {
                    model.ClinicDto.AddRange(detail);
                }
            }
            ViewBag.clinic = clinicname;
            ViewBag.role = rolename;
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> SearchPatient(string searchtext)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_globalClass.Token);
            string rolename = jwt.Claims.First(c => c.Type == "rolename").Value;
            string Doctorid = jwt.Claims.First(c => c.Type == "doctorid").Value;
            var Doctor = await _doctorServices.GetById(Convert.ToInt32(Doctorid));
            var clinicid = Doctor.ClinicId;

            var Patients = await _patientServices.GetAllPatient();
                var result = Patients.Where(x => x.Name == searchtext || x.MobileNo == searchtext && x.ClinicId== clinicid).ToList();
                return Json(result);
        }
        public IActionResult Patient()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Autocomplete(string searchtext)
        {
            var AllPatients = await _patientServices.GetAllPatient();
            var patients = AllPatients.Where(x=>x.Name.Contains(searchtext) || x.MobileNo.Contains(searchtext)).Select(x=>x.Name).ToList();
            return Json(patients);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatient(PatientViewModel model)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_globalClass.Token);
            string Doctorid = jwt.Claims.First(c => c.Type == "doctorid").Value;
            var Doctor = await _doctorServices.GetById(Convert.ToInt32(Doctorid));
            var clinicid = Doctor.ClinicId;

            var patients = await _patientServices.GetAllPatient();
            var patient = patients.Where(x => x.ClinicId == clinicid).ToList();
            if (patient != null && patient.Count() > 0)
            {
                model.PatientDtos.AddRange(patient);
            }
            return View(model);
        }
       
        public async Task<IActionResult> GetPatient(int id)
        {
            var Patient = await _patientServices.GetById(id);
            return Json(Patient);
        }
        public async Task<IActionResult> GetPatintDignosis(int id)
        {
            var dignosis = await _patientDiagnosisServices.GetAll();
            var patient = dignosis.Where(x => x.PatientId == id).OrderByDescending(x => x.Id).Take(2).ToList(); // return only last entry from database
            return Json(patient);
        }
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientDto PatientDto, [FromQuery] bool IsQueue)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_globalClass.Token);
            string rolename = jwt.Claims.First(c => c.Type == "rolename").Value;
            string Doctorid = jwt.Claims.First(c => c.Type == "doctorid").Value;
            var Doctor = await _doctorServices.GetById(Convert.ToInt32(Doctorid));
            var clinicid = Doctor.ClinicId;
            if (rolename == "Receptionist" || rolename == "Doctor")
            {
                int pid;
                if(PatientDto.PatientId == 0)
                {
                    PatientDto patient = new PatientDto
                    {
                        Name = PatientDto.Name,
                        MobileNo = PatientDto.MobileNo,
                        Address = PatientDto.Address,
                        Weight = PatientDto.Weight,
                        Height = PatientDto.Height,
                        SmokingOrNicotine = PatientDto.SmokingOrNicotine,
                        Physically_abled = PatientDto.Physically_abled,
                        isDiabatice = PatientDto.isDiabatice,
                        BP = PatientDto.BP,
                        ClinicId = clinicid
                    };
                    PatientDto results = await _patientServices.Add(patient);
                    pid = results.PatientId;
                }
                else
                {
                    pid = PatientDto.PatientId;
                }
                var Diagnosis = new PatientDiagnosisDto
                {
                    PatientId = pid,
                    IsQueue = IsQueue,
                };
                var result = await _patientDiagnosisServices.Add(Diagnosis);
            }

            return Json(new { result = "success" });
        }

        public string UploadFile(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                if (file.Length > 0)
                {
                    string file_path = Path.Combine(Directory.GetCurrentDirectory(), "UploadFile\\");
                    fileName = Path.GetFileName(file.FileName);
                    var fileExtention = Path.GetExtension(fileName);
                    //var FileName = string.Concat(Convert.ToString(Guid.NewGuid()),fileExtention);

                    if (!Directory.Exists(file_path))
                    {
                        Directory.CreateDirectory(file_path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(file_path + fileName))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    return fileName;
                }
            }
            return fileName;
        }
        [HttpPut]
        public async Task<IActionResult> updateDignosis([FromBody] PatientDiagnosisDto model)
        {
            int dignosisid = model.Id;
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_globalClass.Token);
            string rolename = jwt.Claims.First(c => c.Type == "rolename").Value;
            if(rolename == "Doctor")
            {
                var dignosis = new PatientDiagnosisDto
                {
                    PatientId = model.PatientId,
                    VisitedDateTime = DateTime.Now,
                    Comments = model.Comments,
                    //   Reports= model.Reports,
                    prescription = model.prescription,
                    FoodSuggestions = model.FoodSuggestions,
                    IsQueue = model.IsQueue,
                };
                var result = await _patientDiagnosisServices.Update(dignosisid, dignosis);
                if (result != null)
                {
                    return Json(new { result = "success" });
                }
                else
                {
                    return Json(new { result = "failure" });
                }
            }
            return Json(new { result = "failure" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
