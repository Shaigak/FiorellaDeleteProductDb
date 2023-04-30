using EntityFrameWork.Models;
using System.Collections;

namespace EntityFrameWork.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<List<Product>> GetPaginatedDatas(int page , int take);
        Task<Product> GetFullDataById(int id);

        Task<int> GetCountAsync();
    }
}
