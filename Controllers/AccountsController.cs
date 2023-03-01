using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    public class AccountsController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                else 
                { 
                    return RedirectToAction(nameof(Login));}
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser 
                { 
                    Id = model.Id,
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                };
                user.Id = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Claim claim = new Claim("User", "User");
                    await _userManager.AddClaimAsync(user, claim);

                    await _signInManager.SignInAsync(user,true);
                    return RedirectToAction("Index", "Home");
                }else
                {
                    return View(model);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
