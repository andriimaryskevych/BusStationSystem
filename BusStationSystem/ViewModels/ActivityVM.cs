using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationSystem.DAL.Entities;

namespace BusStationSystem.ViewModels
{
    public class ActivityVM
    {
        public string Name { get; set; }

        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
