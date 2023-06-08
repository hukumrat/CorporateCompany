using CorporateCompany.Areas.Admin.Models;
using CorporateCompany.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace CorporateCompany.Areas.Admin.Data;

public class AppRepository : IAppRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

    public AppRepository(DataContext context, ITempDataDictionaryFactory tempDataDictionaryFactory, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _tempDataDictionaryFactory = tempDataDictionaryFactory;
        _httpContextAccessor = httpContextAccessor;
    }
    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
        SaveAll();
    }

    public void Delete<T>(T entity)
    {
        _context.Remove(entity);
        SaveAll();
    }

    public void IziToast(string message, string header, string type = "success")
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);
        tempData["Message"] = message;
        tempData["Header"] = header;
        tempData["Type"] = type;
    }

    public bool SaveAll()
    {
        return _context.SaveChanges() > 0;
    }

    public Article? GetArticleById(int id)
    {
        var article = _context.Articles.FirstOrDefault(a => a.Id == id);
        return article;
    }

    public List<Content> GetReferences()
    {
        var references = _context.References.ToList();
        List<Content> referenceContents = new List<Content>();
        foreach (var reference in references)
        {
            var content = GetContentById(reference.ContentId);
            referenceContents.Add(content);
        }
        return referenceContents;
    }

    public Content GetContentById(int id)
    {
        var content = _context.Contents.Include(c=>c.Photos).FirstOrDefault(c => c.Id == id);
        return content;
    }

    public List<Photo> GetPhotosByContentId(int id)
    {
        var photos = _context.Photos.Where(p => p.ContentId == id).ToList();
        return photos;
    }

    public Photo GetPhotoById(int id)
    {
        var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
        return photo;
    }

    public Reference GetReferenceById(int id)
    {
        var reference = _context.References.FirstOrDefault(r => r.ContentId == id);
        return reference;
    }

    public List<Content> GetProjects()
    {
        var projects = _context.Projects.ToList();
        List<Content> projectContents = new List<Content>();
        foreach (var project in projects)
        {
            var content = GetContentById(project.ContentId);
            projectContents.Add(content);
        }
        return projectContents;
    }

    public Project GetProjectById(int id)
    {
        var project = _context.Projects.Include(p=>p.Content).FirstOrDefault(p => p.ContentId == id);
        return project;
    }

    public Message GetMessageById(int id)
    {
        var message = _context.Messages.FirstOrDefault(m => m.Id == id);
        return message;
    }

    public List<Message> GetMessages()
    {
        var messages = _context.Messages.ToList();
        return messages;
    }

    public List<Education> GetEducations()
    {
        var educations = _context.Educations.ToList();
        return educations;
    }

    public List<Employment> GetApplications()
    {
        var applications = _context.Employments.ToList();
        return applications;
    }

    public Employment GetApplicationById(int id)
    {
        var application = _context.Employments.FirstOrDefault(e => e.Id == id);
        return application;
    }

    public List<ApplicationUser> GetUsers()
    {
        var users = _context.Users.ToList();
        return users;
    }

    public ApplicationUser GetUserById(string id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        return user;
    }

    public List<Static> GetStatics()
    {
        var statics = _context.Statics.ToList();
        return statics;
    }

    public List<Slide> GetSlides()
    {
        var slides = _context.Slides.ToList();
        return slides;
    }

    public Slide GetSlideById(int id)
    {
        var slide = _context.Slides.FirstOrDefault(s => s.Id == id);
        return slide;
    }

    public string GetSlidePhotoById(int id)
    {
        var slidePhoto = _context.Slides.FirstOrDefault(s=>s.Id == id)?.Photo;
        return slidePhoto;
    }

    public FooterViewModel GetFooter()
    {
        FooterViewModel model = new FooterViewModel()
        {
            AboutUsSummary = _context.Statics.FirstOrDefault()?.AboutUsSummary,
            FacebookUrl = _context.Statics.FirstOrDefault()?.Facebook,
            TwitterUrl = _context.Statics.FirstOrDefault()?.Twitter,
            InstagramUrl = _context.Statics.FirstOrDefault()?.Instagram,
            LinkedInUrl = _context.Statics.FirstOrDefault()?.LinkedIn
        };
        return model;
    }


    public List<Content> GetServices()
    {
        var services = _context.Services.ToList();
        List<Content> serviceContents = new List<Content>();
        foreach (var service in services)
        {
            var content = GetContentById(service.ContentId);
            serviceContents.Add(content);
        }
        return serviceContents;
    }

    public Service GetServiceById(int id)
    {
        var service = _context.Services.FirstOrDefault(s => s.ContentId == id);
        return service;
    }
}