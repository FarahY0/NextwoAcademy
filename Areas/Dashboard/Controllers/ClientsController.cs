using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nextwo.Data;
using Nextwo.Models;
using Nextwo.Models.ViewModel;

namespace Nextwo.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _hostEnvironment;
        public ClientsController(AppDbContext context , IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Dashboard/Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Dashboard/Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Dashboard/Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                string ImgName = UploadeNewFile(model);
                Client client = new Client
                {
                     ClientName=model.ClientName,
                     ClientNote=model.ClientNote,
                     Position=model.Position,
                     ClientImg= ImgName

                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Clients");
            }
            return View(model);
        }
        public string UploadeNewFile(ClientViewModel model)
        {
            string NewFullImgName = null;
            if (model.ClientImg != null)
            {

                string fileRoot = Path.Combine(_hostEnvironment.WebRootPath, @"MyImages\");
                string newFileName = Guid.NewGuid() + "_" + model.ClientImg.FileName;
                string fullpath = Path.Combine(fileRoot, newFileName);
                using (var myNewFile = new FileStream(fullpath, FileMode.Create))
                {
                    model.ClientImg.CopyTo(myNewFile);
                }
                NewFullImgName = @"/MyImages/" + newFileName;
                return NewFullImgName;

            }
            return NewFullImgName;
        }

        // GET: Dashboard/Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Dashboard/Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,ClientName,ClientNote,Position,ClientImg,CreationDate,IsDeleted,IsActive")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Dashboard/Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Dashboard/Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
