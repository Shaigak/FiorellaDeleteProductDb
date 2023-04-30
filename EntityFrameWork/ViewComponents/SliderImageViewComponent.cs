using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameWork.ViewComponents
{
    public class SliderImageViewComponent:ViewComponent
    {
        private readonly ISliderImageService _sliderImageService;

        
        public SliderImageViewComponent(ISliderImageService sliderImageService)
        {
            _sliderImageService = sliderImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //List<Slider> sliders = _context.Sliders.ToList();
            return await Task.FromResult(View(await  _sliderImageService.GetImage()));
        }


    }
}
