using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace CorporateCompany.ViewComponents;

public class ContactUsViewComponent:ViewComponent
{
    private readonly IAppRepository _appRepository;

    public ContactUsViewComponent(IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public ViewViewComponentResult Invoke()
    {
        var staticContents = _appRepository.GetStatics()?.FirstOrDefault();

        var model = new ContactUsViewModel
        {
            Address = staticContents?.Address ?? "",
            Phone = staticContents?.Phone ?? "",
            CompanyEmail = staticContents?.Email ?? ""
        };
        
        return View(model);
    }
}