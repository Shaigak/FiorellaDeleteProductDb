using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<IEnumerable<Blog>> GetAll()
        {
            return await _context.Blogs.Where(m => !m.SoftDelete).ToListAsync();
        }

        public async Task<BlogHeader> GetBlogHeader()
        {
            return  _context.BlogHeaders.Where(m => !m.SoftDelete).ToArray()[1];
        }
    }
}
