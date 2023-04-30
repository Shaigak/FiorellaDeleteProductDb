using EntityFrameWork.Areas.Admin.View_Models;
using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExpertInfoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWorkerService _workerService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExpertInfoController(AppDbContext context, IWorkerService workerService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _workerService = workerService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
         
            return View(await _workerService.GetWorkers());
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Workers worker)
        {
            try
            {

              
                string fileName = Guid.NewGuid().ToString() + " " + worker.Photo.FileName;

                
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await worker.Photo.CopyToAsync(stream);
                }
                
                worker.Image = fileName;


                await _context.Workers.AddAsync(worker);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }   

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Workers worker = await _context.Workers.FirstOrDefaultAsync(m=>m.Id==id);

            _context.Workers.Remove(worker);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }



        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Workers worker = await _context.Workers.FirstOrDefaultAsync(m => m.Id == id);

            if (worker is null) return NotFound();

            return View(worker);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Workers worker = await _context.Workers.FindAsync(id);
            if (worker is null) return NotFound();

            ExpertUpdateVM expert = new()
            {
                Name = worker.Name,
                Position = worker.Position,
                Image = worker.Image,

            };
            return View(expert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,ExpertUpdateVM exper)
        {
            if (id is null) return BadRequest();
            
            Workers dbWorker = await _context.Workers.FirstOrDefaultAsync(m => m.Id == id);

            if (dbWorker is null) return NotFound();

           ExpertUpdateVM model = new()
            {
                Name = dbWorker.Name,
                Position = dbWorker.Position,
                Image = dbWorker.Image,
            };

            if (dbWorker.Photo != null)
            {
                if (!exper.Photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
                {
                    ModelState.AddModelError("Photo", "File type must be image");

                    return View(dbWorker);

                }

                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", dbWorker.Image);

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                string fileName = Path.Combine(Guid.NewGuid().ToString() + "_" + exper.Photo.FileName);


                string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);


                using (FileStream stream = new FileStream(newPath, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
                {
                    await exper.Photo.CopyToAsync(stream);
                }

                dbWorker.Image = fileName;
            }
            else
            {
                Workers workers = new()
                {
                    Image = dbWorker.Image
                };
               
              
            }

            dbWorker.Name = exper.Name;
            dbWorker.Position = exper.Position;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




    }
}
