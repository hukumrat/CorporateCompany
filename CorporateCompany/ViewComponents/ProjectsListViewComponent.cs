using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace CorporateCompany.ViewComponents
{
    public class ProjectsListViewComponent:ViewComponent
    {
        private readonly IAppRepository _appRepository;

        public ProjectsListViewComponent(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public ViewViewComponentResult Invoke()
        {
            var projects = _appRepository.GetProjects();
            List<ProjectsListViewModel> models = new List<ProjectsListViewModel>();
            foreach (var projectContent in projects)
            {
                var project = _appRepository.GetProjectById(projectContent.Id);
                ProjectsListViewModel model = new ProjectsListViewModel
                {
                    Id = projectContent.Id,
                    Name =projectContent.Name,
                    Description = projectContent.Description,
                    IsFinished = project.IsFinished,
                    Photos = projectContent.Photos
                };
                models.Add(model);
            }
            return View(models);
        }
    }
}
