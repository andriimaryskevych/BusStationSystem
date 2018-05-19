using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationSystem.ViewModels
{
    public class RoutesVM
    {
        public string RouteNumber { get; set; }
        public string RouteType { get; set; }
        public int Destination { get; set; }
        public DateTime DetartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }   
        public string BusId { get; set; }
        public string BusNumber { get; set; }
        public string BusType { get; set; }
        public int PlaceCount { get; set; }
        public string OccupiedPlaceCount { get; set; }
    }
}
