using AutoMapper;
using ExamTask.Dtos.User;
using ExamTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        //public async Task<IActionResult> Index()
        //{
        //    AppUser user = new AppUser { UserName = "Admin" };
        //    await _userManager.CreateAsync(user,"Admin123@");
        //    await _userManager.AddToRoleAsync(user,"admin");
        //    return Json("okei");
        //}
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password incorrect");
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password incorrect");
            }
            return RedirectToAction("Index", "Employee");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Employee");
        }
    }
}

