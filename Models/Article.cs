using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class Article : CommonProp
    {
        public int ArticleId { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter An Article")]
        [Display(Name = "Article Title")]
        public string ArticleTitle { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Article Description")]
        public string ArticleDesc { get; set; }
       [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        [Required]
        [Display(Name = "Article Image")]

        public string ArticleImg { get; set; }
    }
}
