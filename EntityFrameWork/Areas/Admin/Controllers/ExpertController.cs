using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExpertController : Controller
    {
        

        private readonly AppDbContext _context;
        private readonly IFlowerService _flowerService;

        public ExpertController(AppDbContext Context, IFlowerService flowerService)
        {
            _context = Context;
            _flowerService = flowerService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Flower> flowers = await _flowerService.GetFlower();
            return View(flowers);

        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Flower flower = await _context.Flowers.FindAsync(id);

            if (flower == null) return NotFound();

            _context.Flowers.Remove(flower);
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flower flower)
        {


            await _context.Flowers.AddAsync(flower);
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null) return BadRequest();
            Flower flower = await _context.Flowers.FindAsync(id);
            if (flower is null) return NotFound();

            return View(flower);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int? id,Flower flower)
        {

            if (id is null) return BadRequest();
            Flower dbFlower =  await _context.Flowers.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (dbFlower is null) return NotFound();
            _context.Flowers.Update(flower);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id is null) return BadRequest();
            Flower flower = await _context.Flowers.FindAsync(id);
            if (flower is null) return NotFound();

            return View(flower);
        }
















    }
}
