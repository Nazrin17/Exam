using AutoMapper;
using ExamTask.DAL.Repository.Interfaces;
using ExamTask.Dtos.Employee;
using ExamTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public HomeController(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee> emps =  await _repository.GetAllAsync();
            List<EmployeeGetDto> getDtos = _mapper.Map<List<EmployeeGetDto>>(emps);
            return View(getDtos);
        }

       
    }
}