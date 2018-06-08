using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string EmploeeId { get; set; }
        public DateTime Dater { get; set; }
        public DateTime Date { get; set; }
        public string Hours { get; set; }
    }
}
