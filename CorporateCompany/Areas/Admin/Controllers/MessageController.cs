using CorporateCompany.Areas.Admin.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCompany.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class MessageController : Controller
{
    private readonly IAppRepository _appRepository;

    public MessageController(IAppRepository appRepository)
    {
        _appRepository = appRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var messages = _appRepository.GetMessages();
        return View(messages);
    }

    [HttpPost]
    public IActionResult Index(int id)
    {
        var messageToDelete = _appRepository.GetMessageById(id);
        if (messageToDelete == null)
        {
            return View("404");
        }
        _appRepository.Delete(messageToDelete);
        _appRepository.IziToast("Mesaj başarıyla silindi.","Başarılı");
        return RedirectToAction("Index", "Message");
    }

    [HttpGet]
    public IActionResult Detail(int id)
    {
        var message = _appRepository.GetMessageById(id);
        if (message == null)
        {
            return View("404");
        }
        return View(message);
    }
}