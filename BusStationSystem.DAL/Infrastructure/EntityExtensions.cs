using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationSystem.DAL.Entities;

namespace BusStationSystem.DAL.Infrastructure
{
    public static class EntityExtensions
    {
        public static string OneLine(this Station station)
        {
            return String.Format("{0}, {1}, {2}", station.Country, station.City, station.StationName);
        }

        public static string OneLine(this Bus bus)
        {
            return String.Format("{0}, {1}, {2}", bus.Make, bus.Model, bus.ProductionYear.Year);
        }
    }    
}
