using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class Role : CommonProp
    {
        public Guid RoleId { get; set; }
        [Required(ErrorMessage ="Enter Role Name")]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
