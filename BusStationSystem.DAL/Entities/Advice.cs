using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusStationSystem.DAL.Entities
{
    public class Advice
    {
        [Key]
        public int Id { get; set; }

        public string AdviceText { get; set; }

        public int EmploeeId { get; set; }
        [ForeignKey("EmploeeId")]
        public User Employee { get; set; }
    }
}
