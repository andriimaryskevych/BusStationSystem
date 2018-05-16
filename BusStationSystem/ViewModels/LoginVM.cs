using BusStationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationSystem.ViewModels
{
    public class LoginVM : UserVM
    {
        [Display(Name = "Запам'ятати?")]
        public bool RememberMe { get; set; }
    }
}
