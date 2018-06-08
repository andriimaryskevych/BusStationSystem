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
        public int Id { get; set; }

        public int DepartureId { get; set; }
        [ForeignKey("DepartureId")]
        public Station Departure { get; set; }
        public DateTime DetartureDate { get; set; }

        public int ArrivalId { get; set; }
        [ForeignKey("ArrivalId")]
        public Station Arrival { get; set; }        
        public DateTime ArrivalDate { get; set; }
        
        public int BusId { get; set; }
        [ForeignKey("BusId")]
        public Bus Bus { get; set; }

        public int Price { get; set; }
    }
}
