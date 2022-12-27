using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class UserCourse : CommonProp
    {
        [Key]
        [Column(Order =0)]
        public Guid UserId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int CourseId { get; set; }

        public virtual User User { get; set; }
        public virtual Course  Course { get; set; }
    }
}
