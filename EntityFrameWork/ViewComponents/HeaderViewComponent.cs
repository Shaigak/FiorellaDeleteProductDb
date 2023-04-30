using EntityFrameWork.Data;
using EntityFrameWork.Services.Interfaces;
using EntityFrameWork.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameWork.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {

        private readonly ILayoutService _layoutService;

        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = _layoutService.GetSettingDatas();
            return await Task.FromResult(View(data));
        }

    }
}
