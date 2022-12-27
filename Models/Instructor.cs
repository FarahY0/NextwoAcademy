using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class Instructor : CommonProp
    {
        public int InstructorId { get; set; }
        [Required(ErrorMessage ="Enter your Name")]
        [Display(Name = "Trainer Name")]

        public string InstructorName { get; set; }
        [Required(ErrorMessage ="Select img")]
        [Display(Name = "Trainer Image")]
        public string InstructorImg { get; set; }
        [Display(Name = "Position")]
        public string InstructorPosition { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
    }
}
