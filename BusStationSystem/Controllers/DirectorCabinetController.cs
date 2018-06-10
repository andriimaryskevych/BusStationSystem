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
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public IActionResult Index()
        {
            return View("Index", new DirectorIndexVM ()
                {
                    Message = "Directors Cainet"
                }
            );
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
                    var currentUser = await _userManager.FindByNameAsync(user.UserName);

                    var roleresult = await _userManager.AddToRoleAsync(currentUser, "employee");

                    return View();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToAction("ViewPersonal");
        }

        public IActionResult ViewBuses()
        {
            var buses = _unitOfWork.Buses.GetAll();

            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"];
            }
            return View(buses);
        }

        public IActionResult AddBus()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditBus(int id)
        {
            Bus bus = _unitOfWork.Buses.Get(id);

            return View(bus);
        }

        [HttpPost]
        public IActionResult AddBus(Bus bus)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Buses.Create(bus);
                _unitOfWork.Save();

                return RedirectToAction("ViewBuses");
            }
            return View(bus);
        }

        [HttpPost]
        public IActionResult EditBus([Bind("Id,Make,Model,ProductionYear")] Bus updatedEntity)
        {
            Console.WriteLine($"{updatedEntity.Id} {updatedEntity.Make} {updatedEntity.Model} {updatedEntity.ProductionYear}");
            if (ModelState.IsValid)
            {
                _unitOfWork.Buses.Update(updatedEntity);
                _unitOfWork.Save();

                return RedirectToAction("ViewBuses", "DirectorCabinet");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteBus(int id)
        {
            Bus bus = _unitOfWork.Buses.Get(id);
            if (bus != null)
            {
                try
                {
                    _unitOfWork.Buses.Delete(bus);
                    _unitOfWork.Save();
                }
                catch (Exception)
                {
                    TempData["message"] = "Operation not possible, bus is in use";
                }
            }
            return RedirectToAction("ViewBuses");
        }


        public IActionResult ViewStations()
        {
            var stations = _unitOfWork.Stations.GetAll();

            return View(stations);
        }

        public IActionResult AddStation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStation(Station station)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Stations.Create(station);
                _unitOfWork.Save();

                return RedirectToAction("ViewStations");
            }
            return View(station);
        }
        
        [HttpPost]
        public ActionResult DeleteStation(int id)
        {
            Station station = _unitOfWork.Stations.Get(id);
            if (station != null)
            {
                _unitOfWork.Stations.Delete(station);
                _unitOfWork.Save();
            }
            return RedirectToAction("ViewStations");
        }

        public async Task<IActionResult> ViewPersonal()
        {
            var users = _userManager.Users.ToList();
            var viewUsers = new List<UserVM>();

            foreach (var item in users)
            {
                bool isDirector = await _userManager.IsInRoleAsync(item, Roles.director.ToString());
                if (!isDirector)
                {
                    var viewUser = new UserVM
                    {
                        Id = item.Id,
                        UserNameSurname = item.UserName
                    };

                    viewUsers.Add(viewUser);
                }
            }

            return View(viewUsers);
        }


        public IActionResult ViewRoutes()
        {
            Expression<Func<Route, object>>[] expr = new Expression<Func<Route, object>>[] {
                a => a.Bus,
                a => a.Departure,
                a => a.Arrival
            };

            var routes = _unitOfWork.Routes.GetAll(expr);

            Console.WriteLine(routes.FirstOrDefault().Bus.Make);

            return View(routes);
        }

        public IActionResult AddRoute()
        {
            var departures =  from departure in _unitOfWork.Stations.GetAll()
                              select new SelectListItem
                              {
                                  Value = departure.Id.ToString(),
                                  Text = String.Format("{0}, {1}, {2}", departure.Country, departure.City, departure.StationName)
                              };

            var arrivals =   from arrival in _unitOfWork.Stations.GetAll()
                              select new SelectListItem
                              {
                                  Value = arrival.Id.ToString(),
                                  Text = String.Format("{0}, {1}, {2}", arrival.Country, arrival.City, arrival.StationName)
                             };

            var buses =     from bus in _unitOfWork.Buses.GetAll()
                            select new SelectListItem
                            {
                               Value = bus.Id.ToString(),
                                Text = String.Format("{0}, {1}, {2}", bus.Make, bus.Model, bus.ProductionYear)
                            };

            return View("AddRoute", new RouteVM {
                Departures = departures,
                Arrivals = arrivals,
                Buses = buses
            });
        }

        [HttpPost]
        public IActionResult AddRoute(RouteVM model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Routes.Create(new Route {
                    Price = model.Price,
                    DepartureId = int.Parse(model.Departure),
                    DetartureDate = model.DetartureDate,
                    ArrivalId = int.Parse(model.Arrival),
                    ArrivalDate = model.ArrivalDate,
                    BusId = int.Parse(model.Bus)
                });

                _unitOfWork.Save();

                return RedirectToAction("ViewRoutes");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditRoute(int id)
        {
            Route route = _unitOfWork.Routes.Get(id);
            if (route == null)
            {
                return NotFound();
            }

            var departures = from departure in _unitOfWork.Stations.GetAll()
                             select new SelectListItem
                             {
                                 Value = departure.Id.ToString(),
                                 Text = String.Format("{0}, {1}, {2}", departure.Country, departure.City, departure.StationName)
                             };

            var arrivals = from arrival in _unitOfWork.Stations.GetAll()
                           select new SelectListItem
                           {
                               Value = arrival.Id.ToString(),
                               Text = String.Format("{0}, {1}, {2}", arrival.Country, arrival.City, arrival.StationName)
                           };

            var buses = from bus in _unitOfWork.Buses.GetAll()
                        select new SelectListItem
                        {
                            Value = bus.Id.ToString(),
                            Text = String.Format("{0}, {1}, {2}", bus.Make, bus.Model, bus.ProductionYear)
                        };

            return View("EditRoute", new RouteVM
            {
                Id = route.Id,
                Departure = route.DepartureId.ToString(),
                Arrival = route.ArrivalId.ToString(),
                DetartureDate = route.DetartureDate,
                ArrivalDate  =route.ArrivalDate,
                Price = route.Price,
                Departures = departures,
                Arrivals = arrivals,
                Buses = buses
            });
        }

        [HttpPost]
        public IActionResult EditRoute(RouteVM model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Routes.Update(new Route
                {
                    Id = model.Id,
                    Price = model.Price,
                    DepartureId = int.Parse(model.Departure),
                    DetartureDate = model.DetartureDate,
                    ArrivalId = int.Parse(model.Arrival),
                    ArrivalDate = model.ArrivalDate,
                    BusId = int.Parse(model.Bus)
                });
                _unitOfWork.Save();

                return RedirectToAction("ViewRoutes", "DirectorCabinet");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteRoute(int id)
        {
            Route route = _unitOfWork.Routes.Get(id);
            if (route != null)
            {
                try
                {
                    _unitOfWork.Routes.Delete(route);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Operation not possible, bus is in use";
                    return Redirect(ControllerContext.HttpContext.Request.Headers["Referer"].ToString());
                }
                _unitOfWork.Save();
            }
            return RedirectToAction("ViewRoutes");
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
                UserNameSurname = user.UserName
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
        public async Task<ActionResult> DeleteUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("ViewPersonal");
        }
    
        public IActionResult ViewClients()
        {
            var model = _unitOfWork.Clients.GetAll();

            return View(model);
        }

        // by their adress
        public IActionResult ViewClientsByAdress(string adress)
        {
            var model = from ticket in _unitOfWork.Tickets.GetAll()
                        where adress == null || ticket.Client.Adress == adress
                        select ticket.Client;

            return View(model);
        }

        public IActionResult AddClient()
        {
            return View();
        } 

        [HttpPost]
        public IActionResult AddClient(Client client)
        {
            _unitOfWork.Clients.Create(client);
            _unitOfWork.Save();

            return RedirectToAction("ViewClients");
        }

        [HttpPost]
        public ActionResult DeleteClient(int id)
        {
            Client client = _unitOfWork.Clients.Get(id);
            if (client != null)
            {
                _unitOfWork.Clients.Delete(client);
                _unitOfWork.Save();
            }
            return RedirectToAction("ViewClients");
        }
    }
}