using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.ViewComponents
{
    public class FlowerViewComponent:ViewComponent
    {
        private readonly IFlowerService _flowerService;

        public FlowerViewComponent(IFlowerService flowerService)
        {
            _flowerService=flowerService;  
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Flower> flowers = await _flowerService.GetFlower();

            return await Task.FromResult(View(flowers));

        }
    }
}
