using mamba.Models.Base;
using Microsoft.Identity.Client;

namespace mamba.Models
{
    public class Employee:BaseEntity
    {
        public string FullName { get; set; }
        public string FbLink { get; set; }
        public string InstLink { get; set; }
        public string LinkedinLink { get; set; }
        public string Image { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
