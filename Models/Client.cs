using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class Client : CommonProp
    {
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        [Display(Name = "Client Name")]

        public string ClientName { get; set; }
        [Required(ErrorMessage = "Enter Your Review")]
        [Display(Name = "Client Note")]

        public string ClientNote { get; set; }
        public string Position { get; set; }
        [Display(Name = "Client Image")]

        public string ClientImg { get; set; }


    }
}
