using EntityFrameWork.Data;
using EntityFrameWork.Models;
using EntityFrameWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameWork.ViewComponents
{
    public class WorkerViewComponent:ViewComponent
    {
        private readonly IWorkerService _workerService;

        public WorkerViewComponent(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //IEnumerable<Workers> workers = _context.Workers.Where(m => !m.SoftDelete).ToList();

            return await Task.FromResult(View(await _workerService.GetWorkers()));
        }

    }
}
