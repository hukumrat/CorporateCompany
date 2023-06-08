using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.Models;
using CorporateCompany.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCompany.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class SlideController : Controller
{
    private readonly IAppRepository _appRepository;
    private readonly IWebHostEnvironment _environment;

    public SlideController(IAppRepository appRepository, IWebHostEnvironment environment)
    {
        _appRepository = appRepository;
        _environment = environment;
    }

    public IActionResult Index()
    {
        var slides = _appRepository.GetSlides();
        List<SlideViewModel> models = new List<SlideViewModel>();
        foreach (var slide in slides)
        {
            SlideViewModel model = new SlideViewModel();
            model.Id = slide.Id;
            model.Header = slide.Header;
            model.PhotoPath = slide.Photo;
            models.Add(model);
        }
        return View(models);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(SlideViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Photo != null && model.Photo.Count > 0)
            {
                List<string> uploadedPhotoPaths = new List<string>();
                foreach (var modelPhoto in model.Photo)
                {
                    var imagesFolderString = $@"Images\Slider";
                    var imagesFolder = Path.Combine(_environment.WebRootPath, imagesFolderString);
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);
                    var fileExtension = Path.GetExtension(modelPhoto.FileName);
                    var uniqueFileName = Guid.NewGuid() + fileExtension;
                    var filePath = Path.Combine(imagesFolder, uniqueFileName);
                    modelPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                    var imagePath = $"/Images/Slider/{uniqueFileName}";
                    uploadedPhotoPaths.Add(imagePath);
                }

                Slide slide = new Slide()
                {
                    Header = model.Header.Trim(),
                    Photo = uploadedPhotoPaths.FirstOrDefault()
                };
                _appRepository.Add(slide);

            }
            _appRepository.IziToast("Slayt başarıyla eklendi.", "Başarılı");
            return RedirectToAction("Index", "Slide");
        }
        else
        {
            _appRepository.IziToast("Lütfen gerekli alanları doldurunuz.","Uyarı","warning");
            return View(model);
        }

            
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var slide = _appRepository.GetSlideById(id);
        return View(slide);
    }

    [HttpPost]
    public IActionResult Update(Slide slide)
    {
        var slideToUpdate = _appRepository.GetSlideById(slide.Id);
        if (slideToUpdate == null)
        {
            return View("404");
        }
        slideToUpdate.Header = slide.Header;
        _appRepository.SaveAll();
        return RedirectToAction("Index", "Slide");
    }

    [HttpGet]
    public IActionResult Photo(int id)
    {
        var slide = _appRepository.GetSlideById(id);
        if (slide== null)
        {
            return View("404");
        }
        SlideViewModel model = new SlideViewModel()
        {
            Id = slide.Id,
            Header = slide.Header,
            PhotoPath = slide.Photo
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Photo(SlideViewModel model)
    {
        var slide = _appRepository.GetSlideById(model.Id);
        if (slide.Photo == string.Empty)
        {
            if (model.Photo != null && model.Photo.Count > 0)
            {
                List<string> uploadedPhotoPaths = new List<string>();
                foreach (var modelPhoto in model.Photo)
                {
                    var imagesFolderString = $@"Images\Slider";
                    var imagesFolder = Path.Combine(_environment.WebRootPath, imagesFolderString);
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);
                    var fileExtension = Path.GetExtension(modelPhoto.FileName);
                    var uniqueFileName = Guid.NewGuid() + fileExtension;
                    var filePath = Path.Combine(imagesFolder, uniqueFileName);
                    modelPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                    var imagePath = $"/Images/Slider/{uniqueFileName}";
                    uploadedPhotoPaths.Add(imagePath);
                }
                slide.Photo = uploadedPhotoPaths.FirstOrDefault();
                _appRepository.SaveAll();
                _appRepository.IziToast("Görsel başarıyla eklendi.", "Başarılı");
                return RedirectToAction("Index", "Slide");
            }
            _appRepository.IziToast("Bir görsel seçmediniz.", "Bilgi", "info");
            return RedirectToAction("Photo", "Slide");
        }
        _appRepository.IziToast("Lütfen önce slayt görselini siliniz.", "Hata", "error");
        return RedirectToAction("Photo", "Slide", new { @id = model.Id });
    }

    [HttpPost]
    public IActionResult DeletePhoto(int id)
    {
        var slide = _appRepository.GetSlideById(id);
        var slidePhotoToDelete = slide.Photo;
        if (slidePhotoToDelete == null)
        {
            return View("404");
        }
        var webRootPath = _environment.WebRootPath;
        var photoPath = webRootPath + slidePhotoToDelete;
        if (System.IO.File.Exists(photoPath))
        {
            try
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.IO.File.Delete(photoPath);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        slide.Photo = string.Empty;
        _appRepository.SaveAll();
        _appRepository.IziToast("Görsel başarıyla silindi.", "Başarılı");
        return RedirectToAction("Photo", "Slide", new { @id = slide.Id });
    }

    [HttpPost]
    public IActionResult DeleteSlide(int id)
    {
        var slideToDelete = _appRepository.GetSlideById(id);
        if (slideToDelete == null)
        {
            return View("404");
        }
        var webRootPath = _environment.WebRootPath;
        var fileToDelete = webRootPath + slideToDelete.Photo;
        if (System.IO.File.Exists(fileToDelete))
        {
            try
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.IO.File.Delete(fileToDelete);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        _appRepository.Delete(slideToDelete);
        _appRepository.IziToast("Slayt başarıyla silindi.", "Başarılı");
        return RedirectToAction("Index", "Slide");
    }
}