using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nextwo.Data;

namespace Nextwo.ViewComponents
{
    public class ClientViewComponent :ViewComponent
    {
        private AppDbContext db;
        public ClientViewComponent(AppDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Clients.OrderByDescending(x => x.ClientName);
            return View(data);
        }
    }
}
