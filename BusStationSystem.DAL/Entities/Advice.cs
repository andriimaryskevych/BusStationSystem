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

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
