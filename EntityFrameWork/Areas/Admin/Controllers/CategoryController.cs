using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;

        public CategoryController( ICategoryService categoryService, AppDbContext context)
        {
           _categoryService=categoryService;
            _context=context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAll());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {

            try
            {

                //if (!ModelState.IsValid)
                //{
                //    return View();
                //}

                var existdata = await _context.Categories.FirstOrDefaultAsync(m => m.Name.Trim().ToLower() == category.Name.Trim().ToLower());
                if (existdata is not null)
                {

                    ModelState.AddModelError("Name", "This data already exist");
                    return View();
                }
                //int num1 = 1;
                //int num2 = 0;
                //int result = num1 / num2;

                //throw new Exception("Yari yolda qoymadi duzeltdi mellim ");
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                //ViewBag.error=ex.Message;
                return RedirectToAction(nameof(Error));
            }    
        }
        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Category category=await _context.Categories.FindAsync(id);

            if(category == null) return NotFound();

             _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }




        [HttpPost]
      
        public async Task<IActionResult> SoftDelete(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _context.Categories.FindAsync(id);

            if (category == null) return NotFound();

            category.SoftDelete = true;

            await _context.SaveChangesAsync();

            return Ok();

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Category category = await _context.Categories.FindAsync(id);
            if (category is null) return NotFound();

            return View(category);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Category category)
        {
            if (id is null) return BadRequest();
            Category dbCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (category is null) return NotFound();

            if(dbCategory.Name.Trim().ToLower() == category.Name.Trim().ToLower())
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Categories.Update(category);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //[HttpGet]
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id is null) return BadRequest();
        //    Category category = await _context.Categories.FindAsync(id);
        //    if (category is null) return NotFound();

        //    return View(category);
        //}


    }
}
