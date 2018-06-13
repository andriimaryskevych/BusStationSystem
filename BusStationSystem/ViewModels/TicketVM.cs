using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationSystem.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusStationSystem.ViewModels
{
    public class TicketVM
    {
        public int RouteId { get; set; }
        public Route Route { get; set; }

        public int Client { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }
    }
}
