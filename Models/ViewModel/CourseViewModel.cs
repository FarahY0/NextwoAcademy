using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nextwo.Models.SharedProp;
using static Nextwo.Models.Course;

namespace Nextwo.Models.ViewModel
{
    public class CourseViewModel : CommonProp
    {
        

        [Required(ErrorMessage = "Enter Course Name")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "Enter Description")]
        [DataType(DataType.MultilineText)]
        public string CourseDesc { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        public string Duration { get; set; }
        [Required(ErrorMessage = "Enter the hours of the course ")]
        [Display(Name = "Hours")]
        public int CourseHours { get; set; }
        public Venus Venu { get; set; }
        [Required]
        public IFormFile CourseImg { get; set; }
        public string BtnTxt { get; set; }
        [Required(ErrorMessage = "Enter Price")]
        [Display(Name = "Price")]
        public int Price { get; set; }
        public decimal PriceOfferDiscount { get; set; }
    
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
