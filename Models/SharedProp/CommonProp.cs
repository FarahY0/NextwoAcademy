using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nextwo.Models.SharedProp
{
    public class CommonProp
    {
        [Display(Name ="Creation Date")]
        public DateTime CreationDate { get; set; }
        public bool   IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
