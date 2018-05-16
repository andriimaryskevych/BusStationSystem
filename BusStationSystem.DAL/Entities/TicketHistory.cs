using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class TicketHistory
    {
        public int Id { get; set; }
        public string TicketId { get; set; }
        public string EmploeeId { get; set; }
    }
}
