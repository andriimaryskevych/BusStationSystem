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

        public IActionResult Index() => View("Index", "Directors worksplace");

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

        public IActionResult ViewBuses()
        {
            var buses = _unitOfWork.Buses.GetAll();
            var viewBuses = new List<BusVM>();

            foreach (var item in buses)
            {
                var bus = _unitOfWork.Buses.Get(item.BusNumber);
                var viewBus = new BusVM
                {
                    BusNumber = item.BusNumber,
                    Type = bus.Type,
                    PlaceCount = bus.PlaceCount
                };

                viewBuses.Add(viewBus);
            }

            return View(viewBuses);
        }

        public IActionResult AddRoute()
        {
            return View();
        }

        public IActionResult AddBus()
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

        [HttpPost]
        public IActionResult AddBus(BusVM model)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.Buses.Get(model.BusNumber) == null)
                {
                    var entity = new Bus
                    {
                        BusNumber = model.BusNumber,
                        Type = model.Type,
                        PlaceCount = model.PlaceCount
                    };

                    _unitOfWork.Buses.Create(entity);
                    _unitOfWork.Save();

                    return RedirectToAction("ViewBuses");
                }
                else
                {
                    ModelState.AddModelError("", "Use unique id");
                }              

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
                UserNameSurname = user.UserName
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult EditBus(string id)
        {
            Bus bus = _unitOfWork.Buses.Get(id);
            Route route = _unitOfWork.Routes.Find(u => u.BusId == id).FirstOrDefault();
            if (bus == null)
            {
                return NotFound();
            }

            BusVM model = new BusVM
            {
                BusNumber = bus.BusNumber,
                Type = bus.Type,
                PlaceCount = bus.PlaceCount,
                RouteId = route?.RouteNumber
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditBus(BusVM model)
        {
            if (ModelState.IsValid)
            {
                Bus bus = _unitOfWork.Buses.Get(model.BusNumber);
                Route route = _unitOfWork.Routes.Find(u => u.BusId == model.BusNumber).FirstOrDefault();
                if (bus != null)
                {
                    bus.BusNumber = model.BusNumber;

                    _unitOfWork.Buses.Update(bus);

                    return RedirectToAction("ViewBuses", "DirectorCabinet");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditRoute(string id)
        {
            Route route = _unitOfWork.Routes.Get(id);
            if (route == null)
            {
                return NotFound();
            }
            RoutesVM model = new RoutesVM
            {
                RouteNumber = route.RouteNumber,
                RouteType = route.RouteType,
                ArrivalDate = route.ArrivalDate,
                DetartureDate = route.DetartureDate,
                Destination = route.Destination,
                BusNumber = route.BusId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditRoute(RoutesVM model)
        {
            if (ModelState.IsValid)
            {
                Route route = _unitOfWork.Routes.Get(model.RouteNumber);
                if (route != null)
                {
                    route.RouteNumber = model.RouteNumber;
                    _unitOfWork.Routes.Update(route);

                    return RedirectToAction("ViewRoutes", "DirectorCabinet");
                }
            }
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

        [HttpPost]
        public ActionResult DeleteRoute(string id)
        {
            Route route = _unitOfWork.Routes.Get(id);
            if (route != null)
            {
                _unitOfWork.Routes.Delete(route);
                _unitOfWork.Save();
            }
            return RedirectToAction("ViewRoutes");
        }

        [HttpPost]
        public ActionResult DeleteBus(string id)
        {
            Bus bus = _unitOfWork.Buses.Get(id);
            if (bus != null)
            {
                _unitOfWork.Buses.Delete(bus);
                _unitOfWork.Save();
            }
            return RedirectToAction("ViewBuses");
        }


        [HttpGet]
        public IActionResult ViewActivety(string id)
        {
            var user = _unitOfWork.Users.Get(id);
            var logs = _unitOfWork.Logs.Find(p => p.EmploeeId == id).ToList();
            var ticketHistories = _unitOfWork.TicketHistories.Find(p => p.EmploeeId == id).ToList();

            var sailedTickets = new List<SailedTicket>();
            foreach (var item in ticketHistories)
            {
                var ticket = _unitOfWork.Tickets.Get(item.TicketId);
                if (ticket.SaleDate.Month == DateTime.Now.Month)
                {
                    var sailedTicket = new SailedTicket
                    {
                        Id = ticket.Id,
                        ClientId = ticket.ClientId,
                        Category = ticket.Category,
                        PlaceNumber = ticket.PlaceNumber,
                        Price = ticket.Price,
                        RouteNumber = ticket.RouteNumber,
                        SaleDate = ticket.SaleDate
                    };

                    sailedTickets.Add(sailedTicket);
                }
            }

            var totalHours = logs.Where(p => p.Date.Month == DateTime.Now.Month).Sum(p => Int32.Parse(p.Hours));
            var totalTickets = sailedTickets.Count;

            var actievity = new ActievityVM
            {
                EmploeeName = user.UserName,
                TotalHours = totalHours,
                TotalTickets = totalTickets,
                SailedTickets = sailedTickets
            };

            return View(actievity);
        }

        public IActionResult ViewClients(string adress)
        {
            //var clients = _unitOfWork.Clients.GetAll();
            var clients = _unitOfWork.Clients.Find(item => adress == null || item.Adress == adress);
            var tickets = _unitOfWork.Tickets.GetAll();

            var viewModel = new List<ClientVM>();

            foreach (Client a in clients)
            {
                foreach (Ticket ticket in tickets)
                {
                    if (ticket.ClientId == a.Id)
                    {
                        viewModel.Add(
                            new ClientVM
                            {
                                FirstName = a.FirstName,
                                LastName = a.LastName,
                                Adress = a.Adress,
                                TicketId = ticket.Id
                            }
                        );
                    }
                }
                
            }

            return View(viewModel);
        }

        public IActionResult AddClient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddClient(ClientVM client)
        {
            _unitOfWork.Clients.Create(new Client()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Adress = client.Adress
            });
            _unitOfWork.Save();

            return RedirectToAction("ViewClients");
        }
    }
}