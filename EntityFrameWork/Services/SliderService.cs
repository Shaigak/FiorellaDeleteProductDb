using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;


        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SliderInfo>> GetSliderData()
        {
            return await _context.SliderInfos.ToListAsync();
        }
    }
}
