﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class TicketHistory
    {
        [Key]
        public int Id { get; set; }

        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        public int EmploeeId { get; set; }
        [ForeignKey("EmploeeId")]
        public User Emploee { get; set; }
    }
}
