using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class Request : CommonProp
    {
        public int RequestId { get; set; }
        public bool IsAccept { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string NameUser { get; set; }
        public string Email { get; set; }
        public string Status { get; internal set; }
    }
    //[Display(Name = "Order Status")]
    //public Staus OrderStatus { get; set; }


    //public enum Status
    //{
    //    Yes, No, Null, Wait
    //}
}
