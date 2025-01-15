using mamba.Areas.Admin.ViewModels.Employee;
using mamba.Configurations;
using mamba.DAL;
using mamba.Utilities.Enums;
using mamba.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mamba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
           _context = context;
        }
        public async Task <IActionResult>Index()
        {
            List<GetEmployeeVM> employeeVM=await _context.Employees.Where(e=>e.SoftDeleted==false)
                .Include(e=>e.Department)
                .Select(e=> new GetEmployeeVM
                {
                    Id=e.Id,
                    Name=e.FullName,
                    DepartmentName =e.Department.Name,
                    Image=e.Image

                }).ToListAsync();

                
                return View(employeeVM);
        }
        public async Task <IActionResult> Create()
        {

            CreateEmployeeVM employeeVM = new CreateEmployeeVM
            {
                Departments = await _context.Departments.ToListAsync(),
            };
            return View(employeeVM);    
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {

            employeeVM.Departments=await _context.Departments.ToListAsync();
            if (!ModelState.IsValid) return View(employeeVM);
            if (!employeeVM.MainnPhoto.ValidateType("image/"))
            {
                ModelState.AddModelError(nameof(employeeVM.MainnPhoto), "File type is incorrect");
                return View(employeeVM);
            }
            if (!employeeVM.MainnPhoto.ValidateSize(FileSize.MB, 1))
            {
                ModelState.AddModelError(nameof(employeeVM.MainnPhoto), "File size is incorrect");
                return View(employeeVM);
            }
            if (!employeeVM.SecondaryPhoto.ValidateType("image/"))
            {
                ModelState.AddModelError(nameof(employeeVM.SecondaryPhoto), "File type is incorrect");
                return View(employeeVM);
            }
            if (!employeeVM.SecondaryPhoto.ValidateSize(FileSize.MB, 1))
            {
                ModelState.AddModelError(nameof(employeeVM.SecondaryPhoto), "File size is incorrect");
                return View(employeeVM);
            }
        }
    }
}
