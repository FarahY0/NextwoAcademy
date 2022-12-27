using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nextwo.Models.ViewModel
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Enter your Name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
