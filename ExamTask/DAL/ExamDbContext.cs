using ExamTask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExamTask.DAL
{
    public class ExamDbContext:IdentityDbContext<AppUser>
    {
        public ExamDbContext(DbContextOptions options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
