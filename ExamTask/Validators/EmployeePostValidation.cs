using ExamTask.Dtos.Employee;
using FluentValidation;
using Microsoft.AspNetCore.Rewrite;

namespace ExamTask.Validators
{
    public class EmployeePostValidation:AbstractValidator<EmployeePostDto>
    {
        public EmployeePostValidation()
        {
            RuleFor(e => e.formFile).NotEmpty().NotNull();
            RuleFor(e => e.Position).NotEmpty().NotNull();
            RuleFor(e => e.Name).NotEmpty().NotNull();
            RuleFor(e => e.About).NotEmpty().NotNull();
           
        }
    }
}
