namespace WebApplication1.Model
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
