using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly AppDbContext _context;

        public FlowerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flower>> GetFlower()
        {
           return await _context.Flowers.Where(m => !m.SoftDelete).ToListAsync();
        }
    }
}
