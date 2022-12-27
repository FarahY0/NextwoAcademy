using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class Partner : CommonProp
    {
        public int PartnerId { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        [Display(Name = "Partner Name")]

        public string PartnerName { get; set; }
        [Display(Name = "Partner Image")]

        public string PartnerImg { get; set; }
    }
}
