using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models
{
    public class Course : CommonProp
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Enter Course Name")]
        [Display(Name = "Course Name")]

        public string CourseName { get; set; }
        [Required(ErrorMessage = "Enter Description")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Course Description")]


        public string CourseDesc { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]

        public DateTime StartDate { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]

        public TimeSpan StartTime { get; set; }
        public string   Duration { get; set; }
        [Required(ErrorMessage = "Enter the hours of the course ")]
        [Display(Name = "Course Hours")]

        public int CourseHours { get; set; }
        public Venus Venu { get; set; }
        [Required]
        [Display(Name = "Course Image")]

        public string CourseImg { get; set; }
        public string BtnTxt { get; set; }
        [Required(ErrorMessage = "Enter Price")]
        [Display(Name = "Price")]
        public int Price { get; set; }
        public decimal PriceOfferDiscount { get; set; }
        
        public int CategoryId { get; set; } public Category Category { get; set; }
        public virtual  ICollection<User>Users  { get; set; }


    }
    public enum Venus
    {
        Offline, Online, Recorded
    }
  

}
