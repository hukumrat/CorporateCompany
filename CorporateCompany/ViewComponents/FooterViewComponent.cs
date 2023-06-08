using CorporateCompany.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace CorporateCompany.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly IAppRepository _appRepository;

        public FooterViewComponent(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public ViewViewComponentResult Invoke()
        {
            var footerContent = _appRepository.GetFooter();
            return View(footerContent);
        }
    }
}
