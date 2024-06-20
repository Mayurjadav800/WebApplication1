namespace WebApplication1.Model
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }

        // Foreign key
        public int EmployeeId { get; set; }

        // Navigation property
        public Employee Employee { get; set; }
    }
}
