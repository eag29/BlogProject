using BlogProject.Identity;
using BlogProject.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<BlogIdentityUser> _usermanager;
        private readonly SignInManager<BlogIdentityUser> _signInManager;

        public AccountController(UserManager<BlogIdentityUser> usermanager, SignInManager<BlogIdentityUser> signInManager)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _usermanager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index","Admin");
            }
            else
            {
                return View();
            }
        }
    }
}
