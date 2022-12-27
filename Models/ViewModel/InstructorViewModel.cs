using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models.ViewModel
{
    public class InstructorViewModel : CommonProp
    {
        
        [Required(ErrorMessage = "Enter your Name")]
        public string InstructorName { get; set; }
        [Required(ErrorMessage = "Select img")]
        public IFormFile InstructorImg { get; set; }
        public string InstructorPosition { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
    }
}
