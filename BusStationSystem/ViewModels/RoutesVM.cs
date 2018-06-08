using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationSystem.DAL.Entities;

namespace BusStationSystem.ViewModels
{
    public class RoutesVM
    {
        public Station Destination { get; set; }
        public DateTime DetartureDate { get; set; }
        public Station Arrival { get; set; }
        public DateTime ArrivalDate { get; set; }   
        public Bus Bus { get; set; }
    }
}
