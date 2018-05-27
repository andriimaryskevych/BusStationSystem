using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationSystem.ViewModels
{
    public class ActievityVM
    {
        public string EmploeeName { get; set; }
        public int TotalHours { get; set; }
        public int TotalTickets { get; set; }
        public List<SailedTicket> SailedTickets { get; set; }
    }
}
