using CorporateCompany.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace CorporateCompany.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly IAppRepository _appRepository;

        public SliderViewComponent(IAppRepository appRepository)
        {
            this._appRepository = appRepository;
        }

        public ViewViewComponentResult Invoke()
        {
            var slides = _appRepository.GetSlides();
            return View(slides);
        }
    }
}
