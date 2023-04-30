using EntityFrameWork.Models;

namespace EntityFrameWork.Services.Interfaces
{
    public interface IWorkerService
    {
       Task<IEnumerable<Workers>> GetWorkers();
    }
}
