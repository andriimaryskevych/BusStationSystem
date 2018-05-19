using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BusStationSystem.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class UserVM
    {
        public string Id {get; set;}

        [Display(Name = "Ім'я користувача")]
        [Required(ErrorMessage = "Ім'я користувача не введено.")]
        public string UserNameSurname { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль не введений.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
