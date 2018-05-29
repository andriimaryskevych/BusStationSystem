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
namespace BusStationSystem.Components
{
    public class Adress : ViewComponent
    {
        private IUnitOfWork repository;
        public Adress(IUnitOfWork repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            var allAdress = repository.Clients.GetAll();

            return View(
                allAdress.Select(x => x.Adress)
                .Distinct()
                .OrderBy(x => x)
            );
        }
    }
}
