using EntityFrameWork.Models;

namespace EntityFrameWork.Services.Interfaces
{
    public interface IFlowerService
    {
        Task<IEnumerable<Flower>> GetFlower();
    }
}
