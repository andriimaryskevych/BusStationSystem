using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class Destination
    {
        public int Id { get; set; }
        public string Station { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
