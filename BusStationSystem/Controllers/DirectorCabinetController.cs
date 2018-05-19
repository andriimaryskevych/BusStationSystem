using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationSystem.DAL.Entities;
using BusStationSystem.DAL.Interfaces;
using BusStationSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusStationSystem.Controllers
{
    public class DirectorCabinetController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public DirectorCabinetController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.UserName };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult ViewPersonal()
        {
            var users = _userManager.Users.ToList();
            var viewUsers = new List<UserVM>();

            foreach (var item in users)
            {

                var viewUser = new UserVM
                {
                    Id = item.Id,
                    UserNameSurname = item.UserName
                };

                viewUsers.Add(viewUser);
            }

            return View(viewUsers);
        }

        public IActionResult ViewRoutes()
        {
            var routes = _unitOfWork.Routes.GetAll();
            var viewRoutes = new List<RoutesVM>();

            foreach (var item in routes)
            {
                var bus = _unitOfWork.Buses.Get(item.BusId);
                var viewRoute = new RoutesVM
                {
                    RouteNumber = item.RouteNumber,
                    Destination = item.Destination,
                    BusNumber = item.BusId,
                    BusType = bus.Type,
                    PlaceCount = bus.PlaceCount,
                    OccupiedPlaceCount = item.OccupiedPlaceCount
                };

                viewRoutes.Add(viewRoute);
            }

            return View(viewRoutes);
        }

        public IActionResult AddRoute()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRoute(RoutesVM model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Route
                {
                    RouteType = model.RouteType,
                    RouteNumber = model.RouteNumber,
                    Destination = model.Destination,
                    DetartureDate = model.DetartureDate,
                    ArrivalDate = model.ArrivalDate,
                    BusId = model.BusNumber
                };

                _unitOfWork.Routes.Create(entity);
                _unitOfWork.Save();

                return RedirectToAction("ViewRoutes");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            UserVM model = new UserVM
            {
                Id = user.Id,
                UserNameSurname = user.UserName,
            };

            return View(model);
        }

        //дописати зміну паролю
        [HttpPost]
        public async Task<IActionResult> EditUser(UserVM model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.UserName = model.UserNameSurname;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ViewUsers", "AdminCabinet");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("ViewUsers");
        }

    }
}