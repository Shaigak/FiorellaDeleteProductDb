using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        
        public ProductService(AppDbContext context)
        {
            _context = context;
          
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return  await _context.Products.Include(m => m.Images).Where(m => !m.SoftDelete).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
           return await _context.Products.CountAsync();    
        }

        public async Task<Product> GetFullDataById(int id)
        {
            return await  _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Product>> GetPaginatedDatas(int page, int take)
        {
          return await _context.Products.Include(m=>m.Category).Include(m =>m.Images).Skip((page*take)-take).Take(take).ToListAsync();
        }



    }
}
