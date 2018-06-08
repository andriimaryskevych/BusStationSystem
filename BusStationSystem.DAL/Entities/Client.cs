using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
    }
}
