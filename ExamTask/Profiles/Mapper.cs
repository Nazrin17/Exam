using AutoMapper;
using ExamTask.Dtos.Employee;
using ExamTask.Dtos.User;
using ExamTask.Models;

namespace ExamTask.Profiles
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Employee, EmployeeGetDto>();
            CreateMap<EmployeePostDto, Employee>();
            CreateMap<UserRegisterDto, AppUser>();
        }
    }
}
