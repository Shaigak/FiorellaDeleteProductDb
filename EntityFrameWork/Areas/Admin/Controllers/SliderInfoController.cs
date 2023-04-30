using EntityFrameWork.Areas.Admin.View_Models;
using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services;
using EntityFrameWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController:Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderInfoController(AppDbContext context, ISliderService service, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _service=service;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
           
            return View(await _service.GetSliderData());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(SliderInfo sliderInfo )
        {
          

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                string fileName = Guid.NewGuid().ToString() + " " + sliderInfo.Photo.FileName;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await sliderInfo.Photo.CopyToAsync(stream);
                }
                sliderInfo.SignatureImage = fileName;

                await _context.SliderInfos.AddAsync(sliderInfo);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                throw;
            }












        }

        [HttpPost]
        public async Task<IActionResult> Delete(int?id)
        {
            if (id is null) return BadRequest();

            SliderInfo sliderData= await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);


            _context.SliderInfos.Remove(sliderData);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id is null) return BadRequest();
            SliderInfo sliderInfo = await _context.SliderInfos.FindAsync(id);
            if (sliderInfo is null) return NotFound();

            return View(sliderInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            SliderInfo sliderInfo = await _context.SliderInfos.FindAsync(id);
            if (sliderInfo is null) return NotFound();

            SliderUpdateVM model = new()
            {
                SignatureImage = sliderInfo.SignatureImage,
                Title = sliderInfo.Title,
                Description = sliderInfo.Description,
            };
            return View(model);
            
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int? id,SliderUpdateVM sliderInfo)
        {
            if (id is null) return BadRequest();

            SliderInfo dbSliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (dbSliderInfo is null) return NotFound();

            SliderUpdateVM model = new()
            {
                SignatureImage = dbSliderInfo.SignatureImage,
                Title = dbSliderInfo.Title,
                Description = dbSliderInfo.Description,
            };

            if (sliderInfo.Photo != null)
            {

                if (!sliderInfo.Photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
                {
                    ModelState.AddModelError("Photo", "File type must be image");

                    return View(dbSliderInfo);

                }

                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", dbSliderInfo.SignatureImage);


                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                string fileName = Path.Combine(Guid.NewGuid().ToString() + "_" + sliderInfo.Photo.FileName);

                string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);


                using (FileStream stream = new FileStream(newPath, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
                {
                    await sliderInfo.Photo.CopyToAsync(stream);
                }


                dbSliderInfo.SignatureImage = fileName;
            }
            else
            {
                SliderInfo newSlider = new()  
                {
                    SignatureImage = dbSliderInfo.SignatureImage
                };
            }

            dbSliderInfo.Title = sliderInfo.Title;
            dbSliderInfo.Description = sliderInfo.Description;


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


           }


    }
}
