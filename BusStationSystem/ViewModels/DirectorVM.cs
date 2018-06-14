using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationSystem.ViewModels
{
    public class Empl
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class DirectorVM
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public IEnumerable<Empl> Employees { get; set; }
    }
}
