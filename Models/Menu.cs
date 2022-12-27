using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class Menu : CommonProp
    {
        public int MenuId { get; set; }
        [Required(ErrorMessage ="Enter Title")]
        [Display(Name ="Title")]
        public string MenuTitle { get; set; }
        [Required(ErrorMessage = "Enter Sub Title")]
        [Display(Name = "Sub Title")]
        public string MinuSubTitle { get; set; }
        [Required(ErrorMessage = "Enter URL Like http")]
        [Display(Name = "Url")]
        [DataType(DataType.Url)]
        public string MenuUrl { get; set; }

    }
}
