using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCompany.Controllers;

public class DetailController : Controller
{
    private readonly IAppRepository _appRepository;

    public DetailController(IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    public IActionResult Index()
    {
        return Redirect("/#Home");
    }

    [HttpGet]
    public IActionResult Projects(int id)
    {
        var project = _appRepository.GetProjectById(id);
        var footerContent = _appRepository.GetFooter();
        if (project == null)
        {
            return View("404");
        }
        var projectContent = _appRepository.GetContentById(id);
        ProjectsListViewModel model = new ProjectsListViewModel
        {
            Name = projectContent.Name,
            Description = projectContent.Description,
            IsFinished = project.IsFinished,
            Photos = projectContent.Photos,
            AboutUsSummary = footerContent.AboutUsSummary,
            FacebookUrl = footerContent.FacebookUrl,
            TwitterUrl = footerContent.TwitterUrl,
            LinkedInUrl = footerContent.LinkedInUrl,
            InstagramUrl = footerContent.InstagramUrl
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Services(int id)
    {
        var service = _appRepository.GetServiceById(id);
        var footerContent = _appRepository.GetFooter();
        if (service == null)
        {
            return View("404");
        }
        var serviceContent = _appRepository.GetContentById(id);
        ServicesListViewModel model = new ServicesListViewModel
        {
            Name = serviceContent.Name,
            Description = serviceContent.Description,
            Photos = serviceContent.Photos,
            AboutUsSummary = footerContent.AboutUsSummary,
            FacebookUrl = footerContent.FacebookUrl,
            TwitterUrl = footerContent.TwitterUrl,
            LinkedInUrl = footerContent.LinkedInUrl,
            InstagramUrl = footerContent.InstagramUrl
        };
        return View(model);
    }
}