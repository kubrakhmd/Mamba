using mamba.Models;

namespace mamba.Areas.Admin.ViewModels.Employee
{
    public class UpdateEmployeeVM
    {
        public string FullName { get; set; }
        public int? DepartmentId { get; set; }
        public List<Department>? Departments { get; set; }
        public IFormFile MainnPhoto { get; set; }
        public List<int> ImageIds { get; set; }
    }
}
