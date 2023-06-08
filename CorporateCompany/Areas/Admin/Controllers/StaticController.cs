using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.Models;
using CorporateCompany.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCompany.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class StaticController : Controller
{
    private readonly IAppRepository _repository;

    public StaticController(IAppRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var staticContent = _repository.GetStatics()?.FirstOrDefault();
        
        var slides = _repository.GetSlides();
        
        var model = new StaticViewModel
        {
            AboutUsSummary = staticContent?.AboutUsSummary ?? "",
            Phone = staticContent?.Phone ?? "",
            Address = staticContent?.Address ?? "",
            Email = staticContent?.Email ?? "",
            Facebook = staticContent?.Facebook ?? "",
            Twitter = staticContent?.Twitter ?? "",
            Instagram = staticContent?.Instagram ?? "",
            LinkedIn = staticContent?.LinkedIn ?? "",
            Slides = slides
        };
        
        return View(model);
    }

    [HttpPost]
    public IActionResult Index(StaticViewModel model)
    {
        if (ModelState.IsValid)
        {
            var staticContent = _repository.GetStatics()?.FirstOrDefault();
            
            var modelPhoneArray = model.Phone.Trim().Split('-');
            var phone = string.Join(" ", modelPhoneArray);
            
            if (staticContent == null)
            {
                var newStaticContent = new Static
                {
                    
                    AboutUsSummary = model.AboutUsSummary,
                    Phone = phone,
                    Address = model.Address.Trim(),
                    Email = model.Email.Trim(),
                    Facebook = model.Facebook.Trim(),
                    Twitter = model.Twitter.Trim(),
                    Instagram = model.Instagram.Trim(),
                    LinkedIn = model.LinkedIn.Trim()
                };
                
                _repository.Add(newStaticContent);
                _repository.SaveAll();

                return RedirectToAction("Index", "Static");
            }
            
            staticContent.AboutUsSummary = model.AboutUsSummary;
            staticContent.Phone = phone;
            staticContent.Address = model.Address.Trim();
            staticContent.Email = model.Email.Trim();
            staticContent.Facebook = model.Facebook.Trim();
            staticContent.Twitter = model.Twitter.Trim();
            staticContent.Instagram = model.Instagram.Trim();
            staticContent.LinkedIn = model.LinkedIn.Trim();
            _repository.SaveAll();
            _repository.IziToast("Statik içerikleriniz başarıyla güncellendi.","Başarılı");
            return RedirectToAction("Index", "Static");
        }

        return View(model);
    }
}