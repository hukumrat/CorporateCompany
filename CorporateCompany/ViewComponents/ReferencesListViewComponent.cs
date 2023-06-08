using CorporateCompany.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace CorporateCompany.ViewComponents
{
    public class ReferencesListViewComponent:ViewComponent
    {
        private readonly IAppRepository _appRepository;

        public ReferencesListViewComponent(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public ViewViewComponentResult Invoke()
        {
            var referenceContents = _appRepository.GetReferences();
            return View(referenceContents);
        }
    }
}
