using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCompany.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class MissionVisionController : Controller
{
    private IAppRepository _appRepository;

    public MissionVisionController(IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    [HttpGet]
    public IActionResult Index(int id)
    {
        var article = _appRepository.GetArticleById(id);
        if (article == null)
        {
            return View("404");
        }

        return View(article);
    }

    [HttpPost]
    public IActionResult Index(Article article)
    {
        var articleToUpdate = _appRepository.GetArticleById(article.Id);
        articleToUpdate.Description = article.Description;
        _appRepository.SaveAll();
        _appRepository.IziToast("Sayfa başaryla güncellendi.", "Başarılı");
        return RedirectToAction("Index", "MissionVision", new { @id = article.Id });
    }
}