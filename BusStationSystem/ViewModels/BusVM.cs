using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationSystem.ViewModels
{
    public class BusVM
    {
        public int Id { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime ProductionYear { get; set; }
    }
}
