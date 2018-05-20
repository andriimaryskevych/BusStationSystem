using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationSystem.ViewModels
{
    public class BusVM
    {
        public string RouteId { get; set; }
        public string BusNumber { get; set; }
        public string Type { get; set; }
        public int PlaceCount { get; set; }
    }
}
