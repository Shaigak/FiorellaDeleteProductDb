using EntityFrameWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameWork.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly IFooterService _footerService;

        public FooterViewComponent(IFooterService footerService)
        {
            _footerService = footerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await _footerService.GetFooter()));
        }



    }
}
