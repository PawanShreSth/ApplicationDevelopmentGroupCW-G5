using Microsoft.AspNetCore.Mvc;

namespace groupCW.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
