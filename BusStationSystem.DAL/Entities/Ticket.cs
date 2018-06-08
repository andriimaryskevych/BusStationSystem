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

        public int RouteId { get; set; }
        [ForeignKey("RouteId")]
        public Route Route { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        
        public DateTime SaleDate { get; set; }
    }
}
