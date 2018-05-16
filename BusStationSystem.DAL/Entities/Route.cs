using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class Route
    {
        [Key]
        public string RouteNumber { get; set; }
        public string RouteType { get; set; }
        public int Destination { get; set; }
        public DateTime DetartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string BusId { get; set; }
        public string OccupiedPlaceCount { get; set; }
    }
}
