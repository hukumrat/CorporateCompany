using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.Models;
using CorporateCompany.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCompany.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ServicesController : Controller
{
    private readonly IAppRepository _appRepository;
    private readonly IWebHostEnvironment _environment;

    public ServicesController(IAppRepository appRepository, IWebHostEnvironment environment)
    {
        _appRepository = appRepository;
        _environment = environment;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var services = _appRepository.GetServices();
        return View(services);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(ServiceAddViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.PhotosToUpload != null && model.PhotosToUpload.Count > 0)
            {
                List<string> uploadedPhotoPaths = new List<string>();
                foreach (var modelPhoto in model.PhotosToUpload)
                {
                    var imagesFolderString = $@"Images\Services\{model.Name.Trim()}";
                    var imagesFolder = Path.Combine(_environment.WebRootPath, imagesFolderString);
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);
                    var fileExtension = Path.GetExtension(modelPhoto.FileName);
                    var uniqueFileName = Guid.NewGuid() + fileExtension;
                    var filePath = Path.Combine(imagesFolder, uniqueFileName);
                    modelPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                    var imagePath = $"/Images/Services/{model.Name.Trim()}/{uniqueFileName}";
                    uploadedPhotoPaths.Add(imagePath);
                }

                Content content = new Content()
                {
                    Name = model.Name.Trim(),
                    Description = model.Description
                };
                _appRepository.Add(content);
                Service service = new Service()
                {
                    ContentId = content.Id,
                    ShortDescription = model.ShortDescription
                };
                _appRepository.Add(service);
                foreach (var path in uploadedPhotoPaths)
                {
                    Photo photo = new Photo
                    {
                        Path = path,
                        ContentId = content.Id
                    };
                    _appRepository.Add(photo);
                }
            }
            else
            {
                Content content = new Content()
                {
                    Name = model.Name.Trim(),
                    Description = model.Description
                };
                _appRepository.Add(content);
                Service service = new Service()
                {
                    ContentId = content.Id,
                    ShortDescription = model.ShortDescription
                };
                _appRepository.Add(service);
            }
            _appRepository.IziToast("Hizmet başarıyla eklendi.", "Başarılı");
            return RedirectToAction("Index", "Services");
        }
        else
        {
            _appRepository.IziToast("Lütfen gerekli alanları doldurunuz.", "Uyarı", "warning");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var serviceContent = _appRepository.GetContentById(id);
        var service = _appRepository.GetServiceById(serviceContent.Id);
        if (service == null)
        {
            return View("404");
        }
        ServiceAddViewModel model = new ServiceAddViewModel
        {
            Id=serviceContent.Id,
            Name = serviceContent.Name,
            Description = serviceContent.Description,
            ShortDescription = service.ShortDescription,
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Update(ServiceAddViewModel model)
    {
        if (ModelState.IsValid)
        {
            var contentToUpdate = _appRepository.GetContentById(model.Id);
            var serviceToUpdate = _appRepository.GetServiceById(model.Id);
            if (contentToUpdate.Name != model.Name.Trim())
            {
                var imagesFolderString = $@"Images\Services\{contentToUpdate.Name}";
                var imagesFolder = Path.Combine(_environment.WebRootPath, imagesFolderString);
                if (Directory.Exists(imagesFolder))
                {
                    imagesFolderString = $@"Images\Services\{model.Name.Trim()}";
                    var updatedImagesFolder = Path.Combine(_environment.WebRootPath, imagesFolderString);
                    try
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        Directory.Move(imagesFolder, updatedImagesFolder);
                    }
                    catch (Exception) { }
                    var photosToUpdate = _appRepository.GetPhotosByContentId(model.Id);
                    foreach (var photo in photosToUpdate)
                    {
                        string[] pathToSplit = photo.Path.Split('/');
                        var newPath = $"/{pathToSplit[1]}/{pathToSplit[2]}/{model.Name.Trim()}/{pathToSplit[4]}";
                        photo.Path = newPath;
                        _appRepository.SaveAll();
                    }
                }
            }
            contentToUpdate.Name = model.Name.Trim();
            contentToUpdate.Description = model.Description;
            serviceToUpdate.ShortDescription = model.ShortDescription;
            _appRepository.SaveAll();
            _appRepository.IziToast("Hizmet başarıyla güncellendi.", "Başarılı");
        }
        else
        {
            _appRepository.IziToast("Lütfen gerekli alanları doldurunuz.", "Uyarı", "warning");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Photos(int id)
    {
        var content = _appRepository.GetContentById(id);
        if (content == null)
        {
            return View("404");
        }
        var contentPhotos = _appRepository.GetPhotosByContentId(id);
        ContentPhotosUpdateViewModel model = new ContentPhotosUpdateViewModel
        {
            ContentId = content.Id,
            Name = content.Name,
            Photos = contentPhotos
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Photos(ContentPhotosUpdateViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.PhotosToUpload != null && model.PhotosToUpload.Count > 0)
            {
                List<string> uploadedPhotoPaths = new List<string>();
                foreach (var modelPhoto in model.PhotosToUpload)
                {
                    var imagesFolderString = $@"Images\Services\{model.Name}";
                    var imagesFolder = Path.Combine(_environment.WebRootPath, imagesFolderString);
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);
                    var fileExtension = Path.GetExtension(modelPhoto.FileName);
                    var uniqueFileName = Guid.NewGuid() + fileExtension;
                    var filePath = Path.Combine(imagesFolder, uniqueFileName);
                    modelPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                    var imagePath = $"/Images/Services/{model.Name}/{uniqueFileName}";
                    uploadedPhotoPaths.Add(imagePath);
                }

                foreach (var path in uploadedPhotoPaths)
                {
                    Photo photo = new Photo
                    {
                        Path = path,
                        ContentId = model.ContentId
                    };
                    _appRepository.Add(photo);
                }
            }
            _appRepository.IziToast("Görsel başarıyla eklendi.", "Başarılı");
            return RedirectToAction("Photos", "Services");
        }
        else
        {
            _appRepository.IziToast("Bir görsel seçmediniz.", "Bilgi", "info");
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult DeletePhoto(int id)
    {
        var photoToDelete = _appRepository.GetPhotoById(id);
        if (photoToDelete == null)
        {
            return View("404");
        }
        var webRootPath = _environment.WebRootPath;
        var photoPath = webRootPath + photoToDelete.Path;
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
        _appRepository.Delete(photoToDelete);
        _appRepository.IziToast("Görsel başarıyla silindi.", "Başarılı");
        return RedirectToAction("Photos", "Services", new { @id = photoToDelete.ContentId });
    }

    [HttpPost]
    public IActionResult DeleteService(int id)
    {
        var serviceToDelete = _appRepository.GetServiceById(id);
        var contentToDelete = _appRepository.GetContentById(id);
        if (contentToDelete == null)
        {
            return View("404");
        }
        var webRootPath = _environment.WebRootPath;
        var photosPath = $"/Images/Services/{contentToDelete.Name}";
        var folderToDelete = webRootPath + photosPath;
        if (System.IO.Directory.Exists(folderToDelete))
        {
            try
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.IO.Directory.Delete(folderToDelete,true);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        var photosToDelete = _appRepository.GetPhotosByContentId(id);
        foreach (var photo in photosToDelete)
        {
            _appRepository.Delete(photo);
        }
        _appRepository.Delete(serviceToDelete);
        _appRepository.Delete(contentToDelete);
        _appRepository.IziToast("Hizmet başarıyla silindi.", "Başarılı");
        return RedirectToAction("Index", "Services");
    }
}