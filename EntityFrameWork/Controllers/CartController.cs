using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using EntityFrameWork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EntityFrameWork.Controllers
{
    public class CartController : Controller
    {


        private readonly AppDbContext _context;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;

        public CartController(AppDbContext context, IProductService productService, IBasketService basketService)
        {
            _context = context;
            _basketService = basketService;
            _productService = productService;
        }


        public async Task<IActionResult> Index()
        {

            List<BasketVM> basketProducts= _basketService.GetBasketDatas();
          
            List<BasketDetailVM> basketDetails = new();

            foreach (var item in basketProducts)
            {
                Product? dbProduct = await _productService.GetFullDataById(item.Id);

                basketDetails.Add(new BasketDetailVM
                {
                    Id = dbProduct.Id,
                    Name=dbProduct.Name,
                    Description=dbProduct.Description,
                    Price=dbProduct.Price,
                    Count=item.Count,
                    Image=dbProduct.Images.Where(m=>m.IsMain).FirstOrDefault()?.Image,
                    Total=item.Count*dbProduct.Price

                });
            }
             
            return View(basketDetails);
        }

        [ActionName("Delete")]
        public IActionResult DeleteProductFromBasket(int? id)
        {

            if (id is null) return BadRequest();

           _basketService.DeleteProductFromBasket((int)id);

            return Ok(_basketService.GetBasketDatas());
        }



        public IActionResult MinusProductFromBasket(int? id)
        {
            if (id is null) return BadRequest();
            List<BasketVM>? basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            var basketCount = basketProducts.FirstOrDefault(m => m.Id == id).Count--;
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));
            return Ok(basketCount);


        }

        public IActionResult PlusProductFromBasket(int? id)
        {
            if (id is null) return BadRequest();
            List<BasketVM>? basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            var basketCount = basketProducts.FirstOrDefault(m => m.Id == id).Count++;
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));
            return Ok(basketCount);


        }

    }

}
