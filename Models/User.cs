using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class User : CommonProp
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage="Enter your Name")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Mobile { get; set; }
        [Required(ErrorMessage ="Enter your Email ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="Your Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Display(Name = "User Image")]

        public string UserImg { get; set; }
        public GenderZ Gender { get; set; }
        [Display(Name = "Order Status")]

        public StatUs OrderStatus { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

       
       


    }
    public enum GenderZ
    {
        Male, Female
    }
    public enum StatUs
    {
        Yes, No, Null, Wait
    }
}
