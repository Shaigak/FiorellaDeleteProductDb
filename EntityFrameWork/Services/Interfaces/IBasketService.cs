using EntityFrameWork.Models;
using EntityFrameWork.ViewModels;

namespace EntityFrameWork.Services.Interfaces
{
    public interface IBasketService
    {
        List<BasketVM> GetBasketDatas();
        void SetDatasToBasket(BasketVM existProduct, Product dbProduct, List<BasketVM> basket);

        void DeleteProductFromBasket(int id );

    }
}
