using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly AppDbContext _context;

        public WorkerService(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Workers>> GetWorkers()
        {
            return await _context.Workers.Where(m => !m.SoftDelete).ToListAsync();
        }
    }
}
