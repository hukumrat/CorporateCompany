using CorporateCompany.Areas.Admin.Data;
using CorporateCompany.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace CorporateCompany.ViewComponents
{
    public class ServicesListViewComponent:ViewComponent
    {
        private readonly IAppRepository _appRepository;

        public ServicesListViewComponent(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public ViewViewComponentResult Invoke()
        {
            var services = _appRepository.GetServices();
            List<ServicesListViewModel> models = new List<ServicesListViewModel>();
            foreach (var serviceContent in services)
            {
                var service = _appRepository.GetServiceById(serviceContent.Id);
                ServicesListViewModel model = new ServicesListViewModel
                {
                    Id = serviceContent.Id,
                    Name = serviceContent.Name,
                    Description = serviceContent.Description,
                    ShortDescription = service.ShortDescription
                };
                models.Add(model);
            }
            return View(models);
        }
    }
}
