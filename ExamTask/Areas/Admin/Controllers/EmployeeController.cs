using AutoMapper;
using ExamTask.DAL.Repository.Interfaces;
using ExamTask.Dtos.Employee;
using ExamTask.Helpers;
using ExamTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository repository, IMapper mapper, IWebHostEnvironment env)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee> emps=await _repository.GetAllAsync();
            List<EmployeeGetDto> getDtos = _mapper.Map<List<EmployeeGetDto>>(emps);
            return View(getDtos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeePostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return View(postDto);
            }
            Employee emp = _mapper.Map<Employee>(postDto);
            if (!postDto.formFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("formfile", "Please send image");
                return View(postDto);
            }
            string imagename = Guid.NewGuid() + postDto.formFile.FileName;
            string path = Path.Combine(_env.WebRootPath, "assets/img/team", imagename);
            using(FileStream file= new FileStream(path, FileMode.Create))
            {
                postDto.formFile.CopyTo(file);
            }
            emp.Image = imagename;
           await _repository.Create(emp);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Employee emp = await _repository.GetAsynyc(e => e.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            Helper.FileDelete(_env.WebRootPath, "assets/img/team", emp.Image);
            _repository.Delete(emp);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Employee emp = await _repository.GetAsynyc(e => e.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            EmployeeGetDto getDto = _mapper.Map<EmployeeGetDto>(emp);
            EmployeeUpdateDto updateDto = new EmployeeUpdateDto { getDto = getDto };
            return View(updateDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeUpdateDto updateDto)
        {

            Employee emp = await _repository.GetAsynyc(e => e.Id == updateDto.getDto.Id);
            updateDto.getDto = _mapper.Map<EmployeeGetDto>(emp);
            if (!ModelState.IsValid)
            {
                return View(updateDto);
            }
            if (emp == null)
            {
                return NotFound();
            }
            if (updateDto.postDto.formFile != null)
            {
                if (!updateDto.postDto.formFile.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("", "Please send image");
                    return View(updateDto);
                }
                string imagename = Guid.NewGuid() + updateDto.postDto.formFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/img/team", imagename);
                using (FileStream file = new FileStream(path, FileMode.Create))
                {
                    updateDto.postDto.formFile.CopyTo(file);
                }
                Helper.FileDelete(_env.WebRootPath, "assets/img/team", emp.Image);
                emp.Image = imagename;
            }
            emp.Position = updateDto.postDto.Position;
            emp.About = updateDto.postDto.About;
            emp.Name = updateDto.postDto.Name;
            if (updateDto.postDto.TwitterUrl != null) emp.TwitterUrl = updateDto.postDto.TwitterUrl;   
            if (updateDto.postDto.FacebookUrl != null) emp.FacebookUrl = updateDto.postDto.FacebookUrl;   
            if (updateDto.postDto.InstagramUrl != null) emp.InstagramUrl = updateDto.postDto.InstagramUrl;   
            if (updateDto.postDto.LinkedinUrl != null) emp.LinkedinUrl = updateDto.postDto.LinkedinUrl;
            _repository.Update(emp);
            return RedirectToAction(nameof(Index));
        }
    }
}
