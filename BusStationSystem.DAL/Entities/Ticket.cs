using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public int RouteiD { get; set; }
        public virtual Route Route { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public DateTime SaleDate { get; set; }
    }
}
