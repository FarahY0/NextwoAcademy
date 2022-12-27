using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models.ViewModel
{
    public class PartnerViewModel : CommonProp
    {
        [Required(ErrorMessage = "Enter Your Name")]
        [Display(Name = "Partner Name")]

        public string PartnerName { get; set; }
        [Display(Name = "Partner Image")]

        public IFormFile PartnerImg { get; set; }
    }
}

