using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCompany.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class PoliciesController : Controller
{
    private readonly IAppRepository _appRepository;

    public PoliciesController(IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }
    
    [HttpGet]
    public IActionResult Index(int id)
    {
        var article = _appRepository.GetArticleById(id);
        if (article == null)
        {
            return View(new Article());
        }

        return View(article);
    }

    [HttpPost]
    public IActionResult Index(Article article)
    {
        var articleToUpdate = _appRepository.GetArticleById(article.Id);
        articleToUpdate.Description = article.Description;
        _appRepository.SaveAll();
        _appRepository.IziToast("Sayfa başarıyla güncellendi.", "Başarılı");
        return RedirectToAction("Index", "Policies", new { @id = article.Id });
    }
}