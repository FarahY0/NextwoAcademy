using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nextwo.Data;

namespace Nextwo.ViewComponents
{
    public class InstructorViewComponent : ViewComponent
    {
        private AppDbContext db;
        public InstructorViewComponent(AppDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Instructors;
            return View(data);
        }
    }
}
