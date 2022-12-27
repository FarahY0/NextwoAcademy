using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models.ViewModel
{
    public class ChangePasswordViewModel :CommonProp
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Your Password")]
        [Compare("New Password",ErrorMessage ="The Password Does Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
