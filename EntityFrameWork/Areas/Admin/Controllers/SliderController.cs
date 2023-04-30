using EntityFrameWork.Areas.Admin.View_Models;
using EntityFrameWork.Data;
using EntityFrameWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Areas.Admin.Controllers;

[Area("Admin")]
public class SliderController : Controller

{

    private readonly AppDbContext _Context;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public SliderController(AppDbContext Context, IWebHostEnvironment webHostEnvironment)
    {
        _Context = Context;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<IActionResult> Index()
    {
        IEnumerable<Slider> sliders = await _Context.Sliders.ToListAsync();
        return View(sliders);
    }


    [HttpGet]
    public async Task<IActionResult> Detail(int? id)
    {

        if (id == null) return BadRequest();

        Slider? slider = await _Context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

        if (slider is null) return NotFound();

        return View(slider);

    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SliderCreateVM slider)
    {
        if (!ModelState.IsValid)
        {
            return View();  // Eger sekil secmeyibse View return elesin 
        }

        foreach (var photo in slider.Photos)
        {
            if (!photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
            {
                ModelState.AddModelError("Photo", "File type must be image");

                return View();

            }

            //if (photo.Length / 1024 > 200)
            //{
            //    ModelState.AddModelError("Photo", "Image Size must be max 200kb");  // Sekilin 200 kbde boyukduse error mesajini cixartsin 
            //    return View();
            //}
        }

        foreach (var photo in slider.Photos)
        {
            string fileName = Guid.NewGuid().ToString() + " " + photo.FileName; // herdefe yeni ad duzeldirik . 

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName); // root duzeldirik . 

            using (FileStream stream = new FileStream(path, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
            {
                await photo.CopyToAsync(stream);
            }

            Slider newSlider = new()
            {
                Image = fileName
            };

            await _Context.Sliders.AddAsync(newSlider);
        }

        await _Context.SaveChangesAsync(); 

        return RedirectToAction(nameof(Index)); 

    }




    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create(Slider slider)
    //{
    //    try
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return View();  // Eger sekil secmeyibse View return elesin 
    //        }

    //        if (!slider.Photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
    //        {
    //            ModelState.AddModelError("Photo", "File type must be image");

    //            return View();

    //        }

    //        if (slider.Photo.Length / 1024 > 200)
    //        {
    //            ModelState.AddModelError("Photo", "Image Size must be max 200kb");  // Sekilin 200 kbde boyukduse error mesajini cixartsin 
    //            return View();
    //        }





    //        string fileName = Guid.NewGuid().ToString() + " " + slider.Photo.FileName; // herdefe yeni ad duzeldirik . 

    //        string path = Path.Combine(_webHostEnvironment.WebRootPath, "img",fileName); // root duzeldirik . 

    //        using (FileStream stream=new FileStream(path, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
    //        {
    //          await slider.Photo.CopyToAsync(stream);  
    //        }

    //        slider.Image = fileName;  // sliderin imagenisini photoya beraberlesdirek 

    //        await _Context.Sliders.AddAsync(slider);  // gelen slideri bazaya save edek 

    //        await _Context.SaveChangesAsync();  // databazaya sava edek 

    //        return RedirectToAction(nameof(Index)); // Indexe redirect edek 




    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
      
    //}



    [HttpPost]

    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Delete(int? id)
    {

        try
        {
            if (id == null) return BadRequest();
            Slider slider = await _Context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            
            if (slider is null) return NotFound();
            
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.Image); // root duzeldirik . 

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _Context.Sliders.Remove(slider);

            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {

            throw;
        }


    }


    [HttpGet]

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return BadRequest();

        Slider slider = await _Context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

        if (slider is null) return NotFound();

        return View(slider);
    }





    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Slider slider)
    {

        try
        {
            if (id == null) return BadRequest();


            Slider dbSlider = await _Context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (dbSlider is null) return NotFound();


            if (slider.Photo == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!slider.Photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
            {
                ModelState.AddModelError("Photo", "File type must be image");

                return View(dbSlider);

            }


            //if (slider.Photo.Length / 1024 > 1200)
            //{
            //    ModelState.AddModelError("Photo", "Image Size must be max 200kb");  // Sekilin 200 kbde boyukduse error mesajini cixartsin 
            //    return View(dbSlider);
            //}

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", dbSlider.Image); // wekilin rutunu gotururuk old path 


            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            
            string fileName= Path.Combine(Guid.NewGuid().ToString()+ "_" + slider.Photo.FileName);
           
            string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);


            using (FileStream stream = new FileStream(newPath, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
            {
                await slider.Photo.CopyToAsync(stream);
            }

            dbSlider.Image = fileName;  // sliderin imagenisini photoya beraberlesdirek 

            await _Context.SaveChangesAsync();  // databazaya sava edek 

            return RedirectToAction(nameof(Index)); // Indexe redirect edek 

        }
        catch (Exception)
        {

            throw;
        }
   

      

    }


    [HttpPost]

    public async Task<IActionResult> SetStatus(int?id)

    {

        if (id is null) return BadRequest();

        Slider slider = await _Context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

        if (slider is null) NotFound();

        if (slider.SoftDelete)
        {
            slider.SoftDelete = false;
        }
        else
        {
            slider.SoftDelete = true;
        }

        await _Context.SaveChangesAsync();

        return Ok(slider.SoftDelete);


    }








}

