﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class ClientProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
    }
}
