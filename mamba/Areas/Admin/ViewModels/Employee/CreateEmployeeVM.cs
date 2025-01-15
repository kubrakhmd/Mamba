using mamba.Models;

namespace mamba.Areas.Admin.ViewModels.Employee
{
    public class CreateEmployeeVM
    {
        public string FullName {  get; set; }
        public int? DepartmentId { get; set; }   
        public List<Department>?Departments { get; set; }
        public IFormFile MainnPhoto { get; set; }
        public IFormFile SecondaryPhoto { get; set; }
    }
}
