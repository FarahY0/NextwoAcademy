using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class Category : CommonProp 
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Enter Category Name")]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }
    }
}
