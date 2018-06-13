using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationSystem.ViewModels
{
    public class Person
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Id { get; set; }
    }

    public class PersonalVM
    {
        public IEnumerable<Person> Directors { get; set; }
        public IEnumerable<Person> Employees { get; set; }
    }
}
