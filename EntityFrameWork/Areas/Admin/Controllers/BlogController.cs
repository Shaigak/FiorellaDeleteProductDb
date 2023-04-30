using EntityFrameWork.Data;
using EntityFrameWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Blog> blogs = _context.Blogs.Where(m => !m.SoftDelete).ToList();
            return View(blogs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                string fileName = Guid.NewGuid().ToString() + " " + blog.Photo.FileName;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await blog.Photo.CopyToAsync(stream);
                }
                blog.Image = fileName;

                await _context.Blogs.AddAsync(blog);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));


            }
            catch (Exception)
            {

                throw;
            }


            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Blog blog = await _context.Blogs.FindAsync(id);

            if (blog == null) return NotFound();

            _context.Blogs.Remove(blog);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Blog blog = await _context.Blogs.FindAsync(id);
            if (blog is null) return NotFound();

            return View(blog);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Blog blog = await _context.Blogs.FindAsync(id);
            if (blog is null) return NotFound();
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Blog blog)
        {
            if (id is null) return BadRequest();
            Blog dbBlog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (dbBlog is null) return NotFound();
            _context.Blogs.Update(blog);


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }




    }
    }

