using IKEA.DAL.Models.Identity;
using IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Services
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #endregion

        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (!ModelState.IsValid)
                return View(signUpViewModel);

            var user = await userManager.FindByNameAsync(signUpViewModel.UserName);

            if (user is not null)
            {
                ModelState.AddModelError(nameof(signUpViewModel.UserName), "This username is already in use");
                return View(signUpViewModel);
            }

            user = new ApplicationUser()
            {
                Fname = signUpViewModel.Fname,
                Lname = signUpViewModel.Lname,
                UserName = signUpViewModel.UserName,
                Email = signUpViewModel.Email,
                IsAgree = signUpViewModel.IsAgree,

            };

            var Result = await userManager.CreateAsync(user, signUpViewModel.Password);

            if (Result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }

            foreach (var error in Result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(signUpViewModel);

        }

        #endregion

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel signInViewModel)
        {
            if (!ModelState.IsValid)
                return View(signInViewModel);

            var user = await userManager.FindByEmailAsync(signInViewModel.Email);

            if (user is not null) 
            {
            var Result = await signInManager.PasswordSignInAsync(user, signInViewModel.Password ,signInViewModel.RememberMe,true );

                if (Result.IsNotAllowed)
                    ModelState.AddModelError(string.Empty, "Your Account Is Not Allowed");
                if (Result.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "Your Account Is Locked");
                if (Result.Succeeded)
                    return RedirectToAction(nameof(HomeController.Index), "Home");

        
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Account");
            return View(signInViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
