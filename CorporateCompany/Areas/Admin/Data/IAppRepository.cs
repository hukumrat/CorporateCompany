using CorporateCompany.Areas.Admin.Models;
using CorporateCompany.ViewModels;
using CorporateCompany.Areas.Admin.Models;

namespace CorporateCompany.Areas.Admin.Data;

public interface IAppRepository
{
    void Add<T>(T entity) where T : class;
    void Delete<T>(T entity);
    void IziToast(string message, string header, string type = "success");
    bool SaveAll();

    Article? GetArticleById(int id);

    Service GetServiceById(int id);
    List<Content> GetServices();

    List<Content> GetReferences();

    Content GetContentById(int id);

    List<Photo> GetPhotosByContentId(int id);
    Photo GetPhotoById(int id);

    Reference GetReferenceById(int id);

    List<Content> GetProjects();

    Project GetProjectById(int id);

    Message GetMessageById(int id);
    List<Message> GetMessages();

    List<Education> GetEducations();

    List<Employment> GetApplications();
    Employment GetApplicationById(int id);

    List<ApplicationUser> GetUsers();
    ApplicationUser GetUserById(string id);

    List<Static?>? GetStatics();

    List<Slide> GetSlides();
    Slide GetSlideById(int id);
    string GetSlidePhotoById(int id);

    FooterViewModel GetFooter();
}