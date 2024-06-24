using Microsoft.AspNetCore.Identity.Data;

namespace WebApplication1.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        // Navigation property for one-to-one relationship
        public Address Address { get; set; }
        // Navigation property for one-to-many relationship
        public ICollection<Project> Projects { get; set; }
        //navigation property to many to many relatioship
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
      

    }
}
