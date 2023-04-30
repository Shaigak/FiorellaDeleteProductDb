using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EntityFrameWork.Controllers
{
    public class ProductController : Controller

    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _context.Products.Include(m => m.Images).Where(m => !m.SoftDelete).Take(4).ToList();
            return View(products);
        }


        public async Task <IActionResult> LoadMore(int skip )
        {

            IEnumerable<Product> products = await _context.Products
                                                             .Include(m => m.Images)
                                                             .Include(m => m.Category)
                                                             .Where(m => !m.SoftDelete)
                                                             .Skip(skip)
                                                             .Take(4)
                                                             .ToListAsync();
            return PartialView("_ProductsPartial", products);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product? dbProduct = await GetProductById((int)id);

            if (dbProduct == null) return NotFound();

            List<BasketVM> basket = GetBasketDatas();


            BasketVM? existProduct = basket?.FirstOrDefault(m => m.Id == dbProduct.Id);

            SetDatasToBasket(existProduct, dbProduct, basket);

            return Ok();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public List<BasketVM> GetBasketDatas()
        {
            List<BasketVM> basket;

            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }

        private void SetDatasToBasket(BasketVM existProduct, Product dbProduct, List<BasketVM> basket)
        {
            if (existProduct == null)
            {

                basket?.Add(new BasketVM
                {
                    Id = dbProduct.Id,
                    Count = 1,


                });
            }
            else
            {
                existProduct.Count++;
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

        }



    }
}
