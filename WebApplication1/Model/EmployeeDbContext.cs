using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext>options):base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Address>().HasKey(i => i.AddressId);

            // One-to-One relationship between Employee and Address
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Address)
                .WithOne(a => a.Employee)
                .HasForeignKey<Address>(a => a.EmployeeId);

            // One-to-Many relationship between Employee and Project
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId);

            // Many-to-Many relationship between Employee and Skill
            modelBuilder.Entity<EmployeeSkill>()
                .HasKey(es => new { es.EmployeeId, es.SkillId });

            modelBuilder.Entity<EmployeeSkill>()
                .HasOne(es => es.Employee)
                .WithMany(e => e.EmployeeSkills)
                .HasForeignKey(es => es.EmployeeId);

            modelBuilder.Entity<EmployeeSkill>()
                .HasOne(es => es.Skill)
                .WithMany(s => s.EmployeeSkills)
                .HasForeignKey(es => es.SkillId);

            modelBuilder.Entity<Loggin>().HasKey(i=>i.Id);

            //modelBuilder.Entity<Employee>()
            //    .HasOne(e => e.Loggin)
            //    .WithOne(l => l.Employee)
            //    .HasForeignKey<Loggin>(l => l.EmployeeId);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<Loggin> Loggins { get; set; }
    }
}
