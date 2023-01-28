using ExamTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamTask.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.About).IsRequired();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Position).IsRequired();

        }
    }
}
