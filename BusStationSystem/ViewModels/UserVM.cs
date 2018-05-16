using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BusStationSystem.ViewModels
{
    [ExcludeFromCodeCoverage]
    public abstract class UserVM
    {

        [Display(Name = "Ім'я користувача")]
        [Required(ErrorMessage = "Ім'я користувача не введено.")]
        [EmailAddress(ErrorMessage = "Ім'я користувача введено не коректно.")]
        public string UserName { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль не введений.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
