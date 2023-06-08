using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCompany.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly IAppRepository _appRepository;
        private readonly IWebHostEnvironment _environment;

        public ApplicationsController(IAppRepository appRepository, IWebHostEnvironment environment)
        {
            _appRepository = appRepository;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var applications = _appRepository.GetApplications();
            List<ApplicationListViewModel> models = new List<ApplicationListViewModel>();
            foreach (var application in applications)
            {
                var educationStatus = string.Empty;
                switch (application.EducationId)
                {
                    case 1:
                        educationStatus = "Lise";
                        break;
                    case 2:
                        educationStatus = "Ön Lisans";
                        break;
                    case 3:
                        educationStatus = "Lisans";
                        break;
                    case 4:
                        educationStatus = "Yüksek Lisans";
                        break;
                }

                string gender;
                switch (application.Gender)
                {
                    case true:
                        gender = "Erkek";
                        break;
                    case false:
                        gender = "Kadın";
                        break;
                }

                string marriage;
                switch (application.Marriage)
                {
                    case true:
                        marriage = "Evli";
                        break;
                    case false:
                        marriage = "Bekar";
                        break;
                }
                ApplicationListViewModel model = new ApplicationListViewModel
                {
                    Id = application.Id,
                    Name = application.Name,
                    Surname = application.Surname,
                    Email = application.Email,
                    Phone = application.Phone,
                    EducationStatus = educationStatus,
                    Gender = gender,
                    Marriage = marriage,
                    CvPath = application.Cv,
                    Date = application.Date
                    
                };
                models.Add(model);
            }
      
            return View(models);
        }

        [HttpPost]
        public IActionResult DeleteApplication(int id)
        {
            var applicationToDelete = _appRepository.GetApplicationById(id);
            var cvPath = $"{_environment.WebRootPath}{applicationToDelete.Cv}";
            if (System.IO.File.Exists(cvPath))
            {
                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(cvPath);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            _appRepository.Delete(applicationToDelete);
            _appRepository.IziToast("İş başvuru formu başarıyla silindi.","Başarılı");
            return RedirectToAction("Index","Applications");
        }
    }
}
