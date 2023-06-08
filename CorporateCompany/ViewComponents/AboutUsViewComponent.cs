using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace CorporateCompany.ViewComponents;

public class AboutUsViewComponent:ViewComponent
{
    private readonly IAppRepository _appRepository;

    public AboutUsViewComponent(IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public ViewViewComponentResult Invoke()
    {
        var article = _appRepository.GetArticleById(3);
        
        var aboutUsDescription = article?.Description ?? "";
        
        var model = new AboutUsViewModel
        {
            Description = aboutUsDescription
        };
        return View(model);
    }
}