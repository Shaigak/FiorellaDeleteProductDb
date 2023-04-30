using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Services
{
    public class SliderImageService : ISliderImageService
    {

        private readonly AppDbContext _context;


        public SliderImageService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Slider>> GetImage()
        {
            return await _context.Sliders.Where(m => !m.SoftDelete).ToListAsync();
        }
    }
}
