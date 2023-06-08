using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.Models;
using CorporateCompany.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CorporateCompany.Controllers;

public class HomeController : Controller
{
    private readonly IAppRepository _appRepository;
    private readonly IWebHostEnvironment _environment;

    public HomeController(IAppRepository appRepository, IWebHostEnvironment environment)
    {
        _appRepository = appRepository;
        _environment = environment;
    }

    public void GetEducationsForDropdown()
    {
        List<SelectListItem> educations = (from x in _appRepository.GetEducations()
            select new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        ViewBag.Educations = educations;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Contact(ContactUsViewModel model)
    {
        if (ModelState.IsValid)
        {
            Message message = new Message
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Messages = model.Messages
            };
            _appRepository.Add(message);
            _appRepository.IziToast("Mesajınız başarıyla gönderildi.", "Başarılı");
        }
        else
        {
            _appRepository.IziToast("Lütfen gerekli alanları doldurunuz.", "Uyarı", "warning");
        }

        return Redirect("/#Contact");
    }


    [HttpPost]
    public async Task<IActionResult> Employment(EmploymentAddViewModel model)
    {
        if (ModelState.IsValid)
        {
            var modelPhone = model.Phone;
            var phoneStrings = modelPhone.Split('-');
            var phone = string.Join("", phoneStrings);

            bool gender = true;
            if (model.Gender == "1")
            {
                gender = true;
            }
            else if (model.Gender == "0")
            {
                gender = false;
            }

            bool marriage = false;
            if (model.Marriage == "1")
            {
                marriage = true;
            }
            else if (model.Marriage == "0")
            {
                marriage = false;
            }

            var cvPath = string.Empty;
            if (model.Cv != null)
            {
                foreach (var cv in model.Cv)
                {
                    var documentsFolderString = @"Documents\Cv\";
                    var documentsFolder = Path.Combine(_environment.WebRootPath, documentsFolderString);
                    if (!Directory.Exists(documentsFolder))
                        Directory.CreateDirectory(documentsFolder);
                    var uniqueFileName =
                        Guid.NewGuid() + $"_{model.Name} {model.Surname}{Path.GetExtension(cv.FileName)}";
                    var filePath = Path.Combine(documentsFolder, uniqueFileName);
                    cv.CopyTo(new FileStream(filePath, FileMode.Create));
                    cvPath = $"/Documents/Cv/{uniqueFileName}";
                }
            }

            Employment employment = new Employment
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Phone = phone,
                EducationId = Convert.ToInt32(model.EducationId),
                Gender = gender,
                Marriage = marriage,
                Cv = cvPath,
                Date = DateTime.Now
            };
            _appRepository.Add(employment);
            _appRepository.IziToast("Başvuru formunuz başarıyla gönderildi.", "Başarılı");
        }
        else
        {
            _appRepository.IziToast("Lütfen gerekli alanları doldurunuz.", "Uyarı", "warning");
        }

        return Redirect("/#hr");
    }
}