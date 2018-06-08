using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class Station
    {
        [Key]
        public int Id { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string StationName { get; set; }
    }
}
