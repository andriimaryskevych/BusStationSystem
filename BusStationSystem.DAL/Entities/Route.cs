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
        public virtual Station Departure { get; set; }
        public DateTime DetartureDate { get; set; }

        public int ArrivalId { get; set; }
        public virtual Station Arrival { get; set; }
        public DateTime ArrivalDate { get; set; }

        public int BusId { get; set; }
        public virtual Bus Bus { get; set; }

        public int Price { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
