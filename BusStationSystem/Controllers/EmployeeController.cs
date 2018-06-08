using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusStationSystem.BLL.Constants;
using BusStationSystem.DAL.Entities;
using BusStationSystem.DAL.Interfaces;
using BusStationSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Users.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public ViewResult Index() =>
            View("Index", "Employee workspace");

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

        public IActionResult SellTicket(string id)
        {
            TicketVM model = new TicketVM() { RouteNumber = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SellTicket(TicketVM ticket)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.Routes.Get(ticket.RouteNumber) != null)
                {
                    if (_unitOfWork.Clients.GetAll().Any(item => item.FirstName == ticket.ClientName))
                    {
                        var entity = new Ticket
                        {
                            RouteNumber = ticket.RouteNumber,
                            ClientId = _unitOfWork.Clients.Find(item => item.FirstName == ticket.ClientName).FirstOrDefault().Id,
                            SaleDate = System.DateTime.Now
                        };

                        _unitOfWork.Tickets.Create(entity);
                        _unitOfWork.Save();

                        var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

                        var history = new TicketHistory
                        {
                            TicketId = entity.Id,
                            EmploeeId = currentUser.Id
                        };

                        _unitOfWork.TicketHistories.Create(history);
                        _unitOfWork.Save();

                        return RedirectToAction("ViewTickets");
                    }
                }
            }
            return View(ticket);
        }

        public async Task<IActionResult> ViewTickets()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var tickets = _unitOfWork.TicketHistories.GetAll();
            var ticketsSoldByCurrentEmployee = tickets.Where(ticket => ticket.EmploeeId == currentUser.Id);

            return View(ticketsSoldByCurrentEmployee);
        }
    }
}