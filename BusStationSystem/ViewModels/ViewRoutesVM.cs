using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationSystem.ViewModels
{
    public class ViewRoutesVM
    {
        public string Bus { get; set; }

        public string Departure { get; set; }
        public DateTime DepartureDate { get; set; }

        public string Arrival { get; set; }
        public DateTime ArrivalDate { get; set; }

        public int Price { get; set; }
    }
}
