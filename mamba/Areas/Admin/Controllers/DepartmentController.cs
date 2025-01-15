using mamba.Areas.Admin.ViewModels;
using mamba.DAL;
using mamba.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mamba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetDepartmentVM> departmentVMs = await _context
                .Departments.Where(d => !d.SoftDeleted)

                .Include(d => d.Employees).Select(

                d => new GetDepartmentVM
                {
                    Id = d.Id,
                    Name = d.Name,

                }
                )
                 .ToListAsync();

            return View(departmentVMs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentVM departmentVM)
        {
            if (!ModelState.IsValid) return View();


            bool result = await _context.Departments.AnyAsync(d => d.Name.Trim() == departmentVM.Name.Trim());
            if (result)
            {

                ModelState.AddModelError("Name", "Name already Exist");
                    return View();
            }
            Department department = new()
            {
                Name = departmentVM.Name
            };
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null || id < 1) return BadRequest();
            Department existed = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (existed == null) return NotFound();
            if (!ModelState.IsValid) return View();



            return View(existed);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Department departmentVM)
        {

            if (id == null || id < 1) return BadRequest();
            Department existed = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (departmentVM is null) return NotFound();
            if (!ModelState.IsValid) return View();
            bool result = await _context.Departments.AnyAsync(d => d.Id == id && d.Name == departmentVM.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(Department.Name), "Department already exists");
                return View();
            }



            existed.Name = departmentVM.Name;

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }
        public async Task <IActionResult> SoftDelete(int? id)
        {
            if(id == null || id < 1) return BadRequest();
            Department department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (department is null) return NotFound();
            department.SoftDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }

}
