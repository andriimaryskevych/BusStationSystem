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
using BusStationSystem.DAL.Infrastructure;

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
            View("Index");
        
        public IActionResult ViewClients()
        {
            var model = _unitOfWork.Clients.GetAll();

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

        public IActionResult ViewRoutes()
        {
            Expression<Func<Route, object>>[] expr = new Expression<Func<Route, object>>[] {
                a => a.Bus,
                a => a.Departure,
                a => a.Arrival
            };

            var routes = _unitOfWork.Routes.GetAll(expr);
            
            return View(routes);
        }

        public IActionResult SellTicket(string id)
        {
            var clients = from client in _unitOfWork.Clients.GetAll()
                          select new SelectListItem
                          {
                              Value = client.Id.ToString(),
                              Text = String.Format("{0} {1}", client.FirstName, client.LastName)
                          };

            var route = _unitOfWork.Routes.Get(int.Parse(id));

            return View(new TicketVM {
                Clients = clients,
                Route = route
            });
        }

        [HttpPost]
        public async Task<IActionResult> SellTicket(TicketVM ticket)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.Routes.Get(ticket.RouteId) != null)
                {
                    var entity = new Ticket
                    {
                        RouteiD = ticket.RouteId,
                        ClientId = ticket.Client,
                        SaleDate = System.DateTime.Now
                    };

                    _unitOfWork.Tickets.Create(entity);
                    _unitOfWork.Save();

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

                    var history = new TicketHistory
                    {
                        TicketId = entity.Id,
                        EmployeeId = currentUser.Id
                    };

                    _unitOfWork.TicketHistories.Create(history);
                    _unitOfWork.Save();

                    return RedirectToAction("ViewTickets");
                }
            }
            return View(ticket);
        }

        public async Task<IActionResult> ViewTickets()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var hitoryTickets = _unitOfWork.TicketHistories.GetAll();
            var ticketsSoldByCurrentEmployee = hitoryTickets.Where(ticket => ticket.EmployeeId == currentUser.Id);

            return View(ticketsSoldByCurrentEmployee);
        }
    }
}