using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace CorporateCompany.ViewComponents;

public class EmploymentAddViewComponent:ViewComponent
{
    private readonly IAppRepository _appRepository;

    public EmploymentAddViewComponent(IAppRepository appRepository)
    {
        _appRepository = appRepository;
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
    public ViewViewComponentResult Invoke()
    {
        var article = _appRepository.GetArticleById(3);
        
        var policyDescription = article?.Description ?? "";
        
        var model = new EmploymentAddViewModel
        {
            Description = policyDescription
        };
        
        GetEducationsForDropdown();
        
        return View(model);
    }
}