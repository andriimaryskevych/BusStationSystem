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

namespace BusStationSystem.Controllers
{
    [Authorize(Roles = Roles.owner)]
    public class OwnerController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public async Task<IActionResult> DeleteCurrent()
        {
            var users = _userManager.Users.ToList();
            var currentDirector = new DirectorVM();

            foreach (var item in users)
            {
                bool isDirector = await _userManager.IsInRoleAsync(item, Roles.director.ToString());

                if (isDirector)
                {
                    currentDirector.Name = item.UserName;
                    currentDirector.Id = item.Id;
                }
            }

            return View(currentDirector);
        }
        
        public async Task<IActionResult> AddNew()
        {
            var users = _userManager.Users.ToList();
            var model = new DirectorVM();

            var list = new List<Empl>();

            foreach (var item in users)
            {
                bool isDirector = await _userManager.IsInRoleAsync(item, Roles.director.ToString());

                if (isDirector)
                {
                    model.Name = item.UserName;
                    model.Id = item.Id;
                }
                else {
                    list.Add(new Empl
                    {
                        Name = item.UserName,
                        Id = item.Id
                    });
                }
            }
            model.Employees = list;
           
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCurrent(DirectorVM director)
        {
            User user = await _userManager.FindByIdAsync(director.Id);
            
            await _userManager.RemoveFromRoleAsync(user, Roles.director);
            
            await _userManager.AddToRoleAsync(user, Roles.employee);
            return RedirectToAction("AddNew");
        }

        //[HttpPost]
        //public IActionResult AddNew()
        //{
        //    return View();
        //}
    }
}