using mamba.DAL;
using mamba.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mamba.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {

              Employees = await _context.Employees
               
                .Take(2)
                .ToListAsync(),



            };
            return View(homeVM);
        }
    }
}
