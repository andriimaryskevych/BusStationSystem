using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationSystem.DAL.Entities;

namespace BusStationSystem.ViewModels
{
    public class ClientVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public Ticket Ticket { get; set; }
    }
}
