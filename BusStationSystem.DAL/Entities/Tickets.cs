using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class Tickets
    {
        public string Id { get; set; }
        public int RouteNumber { get; set; }
        public string Category { get; set; }
        public int PlaceNumber { get; set; }
        public double Price { get; set; }
        public int ClientId { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
