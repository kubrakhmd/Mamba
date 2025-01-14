using Microsoft.AspNetCore.Mvc;

namespace mamba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
