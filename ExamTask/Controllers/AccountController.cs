using AutoMapper;
using ExamTask.Dtos.User;
using ExamTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        //public async Task<IActionResult> Index()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "user" });
        //    return Json("okeei");
        //}
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }
            AppUser user = _mapper.Map<AppUser>(registerDto);
            var result =await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View(registerDto);
                }
            }
            var result2 = await _userManager.AddToRoleAsync(user, "user");
            if (!result2.Succeeded)
            {
                foreach (var item in result2.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View(registerDto);

                }
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto loginDto)

        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);
            if(user == null)
            {
                ModelState.AddModelError("", "Username or password incorrect");
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password incorrect");
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
