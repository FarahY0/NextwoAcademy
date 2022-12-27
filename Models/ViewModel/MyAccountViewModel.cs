using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nextwo.Models.ViewModel
{
    public class MyAccountViewModel 
    {
        [Required(ErrorMessage = "Enter User Name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Pass")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public IFormFile UserImg { get; set; }
        public string ImgString { get; set; }
    }
}
