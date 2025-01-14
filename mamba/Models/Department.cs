using mamba.Models.Base;

namespace mamba.Models
{
    public class Department:BaseEntity
    {
        public string Name {  get; set; }
        public List<Employee>?Employees { get; set; }
    }
}
