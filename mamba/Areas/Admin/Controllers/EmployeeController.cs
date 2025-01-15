using mamba.Areas.Admin.ViewModels.Employee;

using mamba.DAL;
using mamba.Models;
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

            employeeVM.Departments = await _context.Departments.ToListAsync();
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
            bool result = await _context.Departments.AnyAsync(c => c.Id == employeeVM.DepartmentId);
            if (!result)
            {
                ModelState.AddModelError(nameof(employeeVM.DepartmentId), "Wrong Department!");
                return View(employeeVM);
            }
            Employee employee = new()
            {
                FullName = employeeVM.FullName,
                DepartmentId = employeeVM.DepartmentId.Value

            };
            bool exist = await _context.Departments.AnyAsync(c => c.Id == employeeVM.DepartmentId);
            if (!result)
            {
                ModelState.AddModelError(nameof(employeeVM.DepartmentId), "Wrong Department!");
                return View(employeeVM);
            }


            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task <IActionResult>Update (int? id)
        {
            if (id is null|| id<1) return BadRequest();
            Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                FullName=employee.FullName,
                DepartmentId = employee.DepartmentId,
                Departments = await _context.Departments.ToListAsync()

            };
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult>Update (UpdateEmployeeVM employeeVM,int? id)
        {
            if (id is null || id < 1) return BadRequest();
            Employee existed = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (existed is null) return NotFound();

           employeeVM.Departments = await _context.Departments.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            if (employeeVM.MainnPhoto !=null)
            {
                if (!employeeVM.MainnPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateEmployeeVM.MainnPhoto), "Wrong Format!");
                    return View(employeeVM);
                }
                if (!employeeVM.MainnPhoto.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(UpdateEmployeeVM.MainnPhoto), "Wrong Size!");
                    return View(employeeVM);
                }
            }
            if (existed.DepartmentId != employeeVM.DepartmentId)
            {
                bool result = await _context.Departments.AnyAsync(e => e.Id == employeeVM.DepartmentId);
                if (!result)
                {
                    ModelState.AddModelError(nameof(Department.Id), "This category does not exist!");
                    return View(employeeVM);
                }
            }

            if (employeeVM.ImageIds is null)
            {
                employeeVM.ImageIds = new List<int>();
            }
            existed.FullName = employeeVM.FullName;
          
            existed.DepartmentId = employeeVM.DepartmentId.Value;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
