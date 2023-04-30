using EntityFrameWork.Areas.Admin.View_Models;
using EntityFrameWork.Helpers;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryListController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryListController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
       
        public async Task<IActionResult> Index(int page=1,int take=3)
        {

            List<Category> category = await _categoryService.GetPaginateDatas(page,take);

            List<CategoryListVM> mappedDatas=GetMappedDatas(category);

            int pageCount = await GetPageCountAsync(take);

            Paginate<CategoryListVM> paginatedData = new(mappedDatas, page, pageCount);
 
            return View(paginatedData);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount=await _categoryService.GetCountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }


        private List<CategoryListVM> GetMappedDatas(List<Category> category)
        {
            List<CategoryListVM> categories = new();


            foreach (var cate in category)
            {
                CategoryListVM model = new()
                {
                    Name = cate.Name,
                    Id = cate.Id
                };

                categories.Add(model);
            }
            return categories;

        }
    }
}
