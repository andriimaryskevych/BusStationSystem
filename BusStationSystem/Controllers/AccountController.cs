using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationSystem.DAL.Entities;
using BusStationSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusStationSystem.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public IActionResult Login()
        {
            Console.WriteLine(User.Identity.IsAuthenticated);
            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine(User.IsInRole("director"));
                Console.WriteLine(User.IsInRole("employee"));
                
                if (User.IsInRole("director"))
                {
                    return RedirectToAction("Index", "DirectorCabinet");
                }

                if (User.IsInRole("employee"))
                {
                    return RedirectToAction("Index", "Employee");
                }

                if (User.IsInRole("owner"))
                {
                    return RedirectToAction("Index", "Owner");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserNameSurname, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {                    
                    var user = await _userManager.FindByNameAsync(model.UserNameSurname);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("director"))
                    {
                        return RedirectToAction("Index", "DirectorCabinet");
                    }

                    if (roles.Contains("employee"))
                    {
                        return RedirectToAction("Index", "Employee");
                    }

                    if (roles.Contains("owner"))
                    {
                        return RedirectToAction("Index", "Owner");
                    }

                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильний логін і (або) пароль.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}