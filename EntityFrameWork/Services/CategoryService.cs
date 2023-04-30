using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
           return await _context.Categories.Where(m => !m.SoftDelete).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
           return await _context.Categories.CountAsync();
        }

        public async Task<List<Category>> GetPaginateDatas(int page,int take)
        {
            return await _context.Categories.Skip((page*take)-take).Take(2).ToListAsync();
        }


      
    }
}
