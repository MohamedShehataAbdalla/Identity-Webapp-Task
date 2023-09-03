using IdentityTask.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace IdentityTask.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var users = new UserViewModel
            {
                Users = _userManager.Users
            };
            return View(users);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.UserName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Sign In (User&Pass, Cookes True)
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string? Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null) return NotFound();
            var userEdit = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            };
            return View(userEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                //user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.UserName = model.UserName;
                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "user");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string? Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null) return NotFound();
            var userDelete = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email!,
            };
            return View(userDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserViewModel model)
        {
            
            
                var user = _userManager.Users.FirstOrDefault(x => x.Id == model.Id);
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "user");
                }
            
            return View(model);
        }

    }
}
