using EntityFrameWork.Models;

namespace EntityFrameWork.Services.Interfaces
{
    public interface IFooterService
    {
        Task<IEnumerable<Footer>>GetFooter();
    }
}
