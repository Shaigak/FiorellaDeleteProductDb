using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Services
{
    public class FooterService : IFooterService
    {

        private readonly AppDbContext _context;

        public FooterService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Footer>> GetFooter()
        {
            return await _context.Footers.Where(m => !m.SoftDelete).ToListAsync();
        }
    }
}
