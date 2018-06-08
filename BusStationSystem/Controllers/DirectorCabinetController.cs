using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationSystem.BLL.Constants;
using BusStationSystem.DAL.Entities;
using BusStationSystem.DAL.Interfaces;
using BusStationSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusStationSystem.Controllers
{
    [Authorize(Roles = Roles.director)]
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

        //public IActionResult Index() => View("Index", "Directors worksplace");

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = new User { UserName = model.UserName };

        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            var currentUser = await _userManager.FindByNameAsync(user.UserName);

        //            var roleresult = await _userManager.AddToRoleAsync(currentUser, "employee");

        //            return View();
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return RedirectToAction("ViewPersonal");
        //}

        //public async Task<IActionResult> ViewPersonal()
        //{
        //    var users = _userManager.Users.ToList();
        //    var viewUsers = new List<UserVM>();

        //    foreach (var item in users)
        //    {
        //        bool isDirector = await _userManager.IsInRoleAsync(item, Roles.director.ToString());
        //        if (!isDirector)
        //        {
        //            var viewUser = new UserVM
        //            {
        //                Id = item.Id,
        //                UserNameSurname = item.UserName
        //            };

        //            viewUsers.Add(viewUser);
        //        }
        //    }

        //    return View(viewUsers);
        //}

        //public IActionResult ViewRoutes()
        //{
        //    var routes = _unitOfWork.Routes.GetAll();

        //    return View(routes);
        //}

        //public IActionResult ViewBuses()
        //{
        //    var buses = _unitOfWork.Buses.GetAll();

        //    return View(buses);
        //}

        //public IActionResult AddRoute()
        //{
        //    return View();
        //}

        //public IActionResult AddBus()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddRoute(Route model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Routes.Create(model);
        //        _unitOfWork.Save();

        //        return RedirectToAction("ViewRoutes");
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult AddBus(Bus model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Buses.Create(model);
        //        _unitOfWork.Save();

        //        return RedirectToAction("ViewBuses");   
        //    }
        //    return View(model);
        //}

        //[HttpGet]
        //public async Task<IActionResult> EditUser(string id)
        //{
        //    User user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    UserVM model = new UserVM
        //    {
        //        Id = user.Id,
        //        UserNameSurname = user.UserName
        //    };

        //    return View(model);
        //}

        //[HttpGet]
        //public IActionResult EditBus(int id)
        //{
        //    Bus bus = _unitOfWork.Buses.Get(id);

        //    return View(bus);
        //}

        //[HttpPost]
        //public IActionResult EditBus(Bus model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Buses.Update(model);

        //        return RedirectToAction("ViewBuses", "DirectorCabinet");               
        //    }
        //    return View(model);
        //}

        //[HttpGet]
        //public IActionResult EditRoute(int id)
        //{
        //    Route route = _unitOfWork.Routes.Get(id);
        //    if (route == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(route);
        //}

        //[HttpPost]
        //public IActionResult EditRoute(Route model)
        //{
        //    if (ModelState.IsValid)
        //    {              
        //        _unitOfWork.Routes.Update(model);

        //        return RedirectToAction("ViewRoutes", "DirectorCabinet");                
        //    }
        //    return View(model);
        //}

        ////дописати зміну паролю
        //[HttpPost]
        //public async Task<IActionResult> EditUser(UserVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await _userManager.FindByIdAsync(model.Id);
        //        if (user != null)
        //        {
        //            user.UserName = model.UserNameSurname;

        //            var result = await _userManager.UpdateAsync(user);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("ViewUsers", "AdminCabinet");
        //            }
        //            else
        //            {
        //                foreach (var error in result.Errors)
        //                {
        //                    ModelState.AddModelError(string.Empty, error.Description);
        //                }
        //            }
        //        }
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<ActionResult> DeleteUser(string id)
        //{
        //    User user = await _userManager.FindByIdAsync(id);
        //    if (user != null)
        //    {
        //        IdentityResult result = await _userManager.DeleteAsync(user);
        //    }
        //    return RedirectToAction("ViewPersonal");
        //}

        //[HttpPost]
        //public ActionResult DeleteRoute(int id)
        //{
        //    Route route = _unitOfWork.Routes.Get(id);
        //    if (route != null)
        //    {
        //        _unitOfWork.Routes.Delete(route);
        //        _unitOfWork.Save();
        //    }
        //    return RedirectToAction("ViewRoutes");
        //}

        //[HttpPost]
        //public ActionResult DeleteBus(int id)
        //{
        //    Bus bus = _unitOfWork.Buses.Get(id);
        //    if (bus != null)
        //    {
        //        _unitOfWork.Buses.Delete(bus);
        //        _unitOfWork.Save();
        //    }
        //    return RedirectToAction("ViewBuses");
        //}
        
        //public IActionResult ViewClients(string adress)
        //{
        //    //var clients = _unitOfWork.Clients.GetAll();
        //    var clients = _unitOfWork.Clients.Find(item => adress == null || item.Adress == adress);
        //    var tickets = _unitOfWork.Tickets.GetAll();

        //    var viewModel = new List<ClientVM>();

        //    foreach (Client a in clients)
        //    {
        //        foreach (Ticket ticket in tickets)
        //        {
        //            if (ticket.ClientId == a.Id)
        //            {
        //                viewModel.Add(
        //                    new ClientVM
        //                    {
        //                        FirstName = a.FirstName,
        //                        LastName = a.LastName,
        //                        Adress = a.Adress
        //                    }
        //                );
        //            }
        //        }
                
        //    }

        //    return View(viewModel);
        //}

        //public IActionResult AddClient()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddClient(ClientVM client)
        //{
        //    _unitOfWork.Clients.Create(new Client()
        //    {
        //        FirstName = client.FirstName,
        //        LastName = client.LastName,
        //        Adress = client.Adress
        //    });
        //    _unitOfWork.Save();

        //    return RedirectToAction("ViewClients");
        //}
    }
}