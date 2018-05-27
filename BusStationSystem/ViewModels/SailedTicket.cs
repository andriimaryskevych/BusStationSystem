using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationSystem.ViewModels
{
    public class SailedTicket
    {
        public string Id { get; set; }
        public string RouteNumber { get; set; }
        public string Category { get; set; }
        public int PlaceNumber { get; set; }
        public double Price { get; set; }
        public int ClientId { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
