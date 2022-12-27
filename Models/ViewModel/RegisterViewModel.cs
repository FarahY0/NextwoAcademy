using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Nextwo.Models.User;

namespace Nextwo.Models.ViewModel
{
    public class RegisterViewModel
    {
        
        [Required(ErrorMessage = "Enter your Name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Enter your Email ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Your Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
