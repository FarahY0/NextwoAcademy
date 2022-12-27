using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models.ViewModel
{
    public class ArticleViewModel : CommonProp
    {
        [Required]
        
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter An Article")]
        public string ArticleTitle { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string ArticleDesc { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        [Required]
        public IFormFile ArticleImg { get; set; }
    }
}
