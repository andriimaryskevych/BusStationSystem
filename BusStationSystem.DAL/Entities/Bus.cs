using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class Bus
    {
        [Key]
        public string BusNumber { get; set; }
        public string Buser { get; set; }
        public string Type { get; set; }
        public int PlaceCount { get; set; }
    }
}
