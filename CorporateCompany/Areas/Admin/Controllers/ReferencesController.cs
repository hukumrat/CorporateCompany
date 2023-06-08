using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.Areas.Admin.Models;
using CorporateCompany.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCompany.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ReferencesController : Controller
{
    private readonly IAppRepository _appRepository;
    private readonly IWebHostEnvironment _environment;

    public ReferencesController(IAppRepository appRepository, IWebHostEnvironment environment)
    {
        _appRepository = appRepository;
        _environment = environment;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var references = _appRepository.GetReferences();
        return View(references);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(ReferenceAddViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.PhotosToUpload != null && model.PhotosToUpload.Count > 0)
            {
                List<string> uploadedPhotoPaths = new List<string>();
                foreach (var modelPhoto in model.PhotosToUpload)
                {
                    var imagesFolderString = $@"Images\References\{model.Name.Trim()}";
                    var imagesFolder = Path.Combine(_environment.WebRootPath, imagesFolderString);
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);
                    var fileExtension = Path.GetExtension(modelPhoto.FileName);
                    var uniqueFileName = Guid.NewGuid() + fileExtension;
                    var filePath = Path.Combine(imagesFolder, uniqueFileName);
                    Directory.CreateDirectory(imagesFolder);
                    modelPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                    var imagePath = $"/Images/References/{model.Name.Trim()}/{uniqueFileName}";
                    uploadedPhotoPaths.Add(imagePath);
                }

                Content content = new Content()
                {
                    Name = model.Name.Trim(),
                    Description = model.Description
                };
                _appRepository.Add(content);

                Reference reference = new Reference()
                {
                    ContentId = content.Id
                };
                _appRepository.Add(reference);
      
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

                Reference reference = new Reference()
                {
                    ContentId =  content.Id
                };
                _appRepository.Add(reference);

            }
            _appRepository.IziToast("Referans başarıyla eklendi.", "Başarılı");
            return RedirectToAction("Index", "References");
        }
        else
        {
            _appRepository.IziToast("Lütfen gerekli alanları doldurunuz.", "Uyarı","warning");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var referenceContent = _appRepository.GetContentById(id);
        if (referenceContent == null)
        {
            return View("404");
        }
        return View(referenceContent);
    }

    [HttpPost]
    public IActionResult Update(Content content)
    {
        if (ModelState.IsValid)
        {
            var contentToUpdate = _appRepository.GetContentById(content.Id);
            if (contentToUpdate.Name.Trim() != content.Name.Trim())
            {
                var imagesFolderString = $@"Images\References\{contentToUpdate.Name.Trim()}";
                var imagesFolder = Path.Combine(_environment.WebRootPath, imagesFolderString);
                if (Directory.Exists(imagesFolder))
                {
                    imagesFolderString = $@"Images\References\{content.Name.Trim()}";
                    var updatedImagesFolder = Path.Combine(_environment.WebRootPath, imagesFolderString);
                    try
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        Directory.Move(imagesFolder, updatedImagesFolder);
                    }
                    catch (Exception) { }
                    var photosToUpdate = _appRepository.GetPhotosByContentId(content.Id);
                    foreach (var photo in photosToUpdate)
                    {
                        var pathToSplit = photo.Path.Split('/');
                        var newPath = $"/{pathToSplit[1]}/{pathToSplit[2]}/{content.Name.Trim()}/{pathToSplit[4]}";
                        photo.Path = newPath;
                        _appRepository.SaveAll();
                    }
                }
            }
            contentToUpdate.Name = content.Name.Trim();
            contentToUpdate.Description = content.Description;
            _appRepository.SaveAll();
            _appRepository.IziToast("Referans başarıyla güncellendi.", "Başarılı");
            return View(contentToUpdate);
        }
        else
        {
            _appRepository.IziToast("Lütfen gerekli alanları doldurunuz.", "Uyarı","warning");
        }
        return View(content);
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
                    var imagesFolderString = String.Format(@"Images\References\{0}", model.Name);
                    var imagesFolder = Path.Combine(_environment.WebRootPath, imagesFolderString);
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);
                    var fileExtension = Path.GetExtension(modelPhoto.FileName);
                    var uniqueFileName = Guid.NewGuid() + fileExtension;
                    var filePath = Path.Combine(imagesFolder, uniqueFileName);
                    modelPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                    var imagePath = String.Format("/Images/References/{0}/{1}", model.Name, uniqueFileName);
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
            _appRepository.IziToast("Görsel başarıyla eklendi.", "Başarılı", "success");
            return RedirectToAction("Photos", "References");
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
        _appRepository.IziToast("Görsel başarıyla silindi.", "Başarılı", "success");
        return RedirectToAction("Photos", "References", new { @id = photoToDelete.ContentId });
    }

    [HttpPost]
    public IActionResult DeleteReference(int id)
    {
        var referenceToDelete = _appRepository.GetReferenceById(id);
        var contentToDelete = _appRepository.GetContentById(id);
        if (contentToDelete == null)
        {
            return View("404");
        }
        var webRootPath = _environment.WebRootPath;
        var photosPath = $"/Images/References/{contentToDelete.Name}";
        var folderToDelete = webRootPath + photosPath;
        if (System.IO.Directory.Exists(folderToDelete))
        {
            try
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.IO.Directory.Delete(folderToDelete, true);
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
        _appRepository.Delete(referenceToDelete);
        _appRepository.Delete(contentToDelete);
        _appRepository.IziToast("Referans başarıyla silindi.", "Başarılı", "success");
        return RedirectToAction("Index", "References");
    }
}