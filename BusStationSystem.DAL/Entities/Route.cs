using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class Route
    {
        [Key]
        public string RouteNumber { get; set; }
        public string RouteType { get; set; }
        public string Destination { get; set; }
        public DateTime DetartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string OccupiedPlaceCount { get; set; }
        public string BusId { get; set; }

        [ForeignKey("BusId")]
        public Bus Bus { get; set; }
    }
}
