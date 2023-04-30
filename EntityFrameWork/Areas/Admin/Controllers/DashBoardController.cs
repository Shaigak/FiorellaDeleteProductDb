using Microsoft.AspNetCore.Mvc;

namespace EntityFrameWork.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
