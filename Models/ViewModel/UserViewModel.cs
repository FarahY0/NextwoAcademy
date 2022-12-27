using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models.ViewModel
{
    public class UserViewModel : CommonProp
    {
        [Required(ErrorMessage = "Enter your Name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Enter your Email ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Your Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public IFormFile UserImg { get; set; }
        public GenderZ Gender { get; set; }
        public StatUs OrderStatus { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
