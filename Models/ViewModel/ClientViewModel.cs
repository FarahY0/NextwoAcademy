using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models.ViewModel
{
    public class ClientViewModel : CommonProp
    {
        [Required(ErrorMessage = "Enter Your Name")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "Enter Your Review")]
        public string ClientNote { get; set; }
        public string Position { get; set; }
        public IFormFile ClientImg { get; set; }
    }
}
