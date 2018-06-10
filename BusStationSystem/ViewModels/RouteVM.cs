using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusStationSystem.ViewModels
{
    public class RouteVM
    {
        public int Id { get; set; }
        public int Price { get; set; }

        public IEnumerable<SelectListItem> Departures { get; set; }
        public IEnumerable<SelectListItem> Arrivals { get; set; }
        public IEnumerable<SelectListItem> Buses { get; set; }

        public DateTime DetartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Bus { get; set; }
    }
}
