using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using EntityFrameWork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public BlogViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }
       
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _blogService.GetAll();

            var blogHeader=await _blogService.GetBlogHeader();

            var model = new BlogVM
            {
                Blogs = blogs,
                BlogHeader = blogHeader,
            };

            return await Task.FromResult(View(model));
        }
        
           //IEnumerable<Blog> blogs = await _context.Blogs.Where(m => !m.SoftDelete).ToListAsync();

            //return await Task.FromResult(View(await _context.Blogs.Where(m => !m.SoftDelete).ToListAsync();));
        
    }
}
