using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nextwo.Data;

namespace Nextwo.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private AppDbContext db;
        public MenuViewComponent(AppDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Menus.OrderByDescending(x => x.CreationDate);
            return View(data);
        }
    }
}
