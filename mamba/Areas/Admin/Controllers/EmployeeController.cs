using Microsoft.AspNetCore.Mvc;

namespace mamba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
