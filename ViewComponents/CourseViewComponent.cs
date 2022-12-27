using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nextwo.Data;

namespace Nextwo.ViewComponents
{
    public class CourseViewComponent : ViewComponent
    {
        private AppDbContext db;
        public CourseViewComponent(AppDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Courses.OrderByDescending(x => x.CreationDate).Take(6);
            return View(data);
        }


        


    }
}
