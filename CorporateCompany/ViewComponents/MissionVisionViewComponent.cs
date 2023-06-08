using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace CorporateCompany.ViewComponents;

public class MissionVisionViewComponent : ViewComponent
{
    private readonly IAppRepository _appRepository;

    public MissionVisionViewComponent(IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public ViewViewComponentResult Invoke()
    {
        var article = _appRepository.GetArticleById(6);
        
        var missionVisionDescription = article?.Description ?? "";
        
        var model = new MissionVisionViewModel
        {
            Description = missionVisionDescription
        };
        
        return View(model);
    }
}