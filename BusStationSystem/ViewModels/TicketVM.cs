using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationSystem.DAL.Entities;

namespace BusStationSystem.ViewModels
{
    public class TicketVM
    {
        public string RouteNumber { get; set; }
        public Client Client { get; set; }
    }
}
