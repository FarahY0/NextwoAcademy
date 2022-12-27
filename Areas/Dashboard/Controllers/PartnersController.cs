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
    public class PartnersController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _hostEnvironment;

        public PartnersController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Dashboard/Partners
        public async Task<IActionResult> Index()
        {
            return View(await _context.Partners.ToListAsync());
        }

        // GET: Dashboard/Partners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partners
                .FirstOrDefaultAsync(m => m.PartnerId == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // GET: Dashboard/Partners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Partners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                string ImgName = UploadeNewFile(model);
                Partner partner = new Partner
                {
                   
                    PartnerImg = ImgName,
                    PartnerName =model.PartnerName
                };
                _context.Partners.Add(partner);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Partners");
            }
            return View(model);
        }
        public string UploadeNewFile(PartnerViewModel model)
        {
            string NewFullImgName = null;
            if (model.PartnerImg != null)
            {

                string fileRoot = Path.Combine(_hostEnvironment.WebRootPath, @"MyImages\");
                string newFileName = Guid.NewGuid() + "_" + model.PartnerImg.FileName;
                string fullpath = Path.Combine(fileRoot, newFileName);
                using (var myNewFile = new FileStream(fullpath, FileMode.Create))
                {
                    model.PartnerImg.CopyTo(myNewFile);
                }
                NewFullImgName = @"/MyImages/" + newFileName;
                return NewFullImgName;

            }
            return NewFullImgName;
        }

        // GET: Dashboard/Partners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partners.FindAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            return View(partner);
        }

        // POST: Dashboard/Partners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartnerId,PartnerName,PartnerImg,CreationDate,IsDeleted,IsActive")] Partner partner)
        {
            if (id != partner.PartnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerExists(partner.PartnerId))
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
            return View(partner);
        }

        // GET: Dashboard/Partners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partners
                .FirstOrDefaultAsync(m => m.PartnerId == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Dashboard/Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            _context.Partners.Remove(partner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerExists(int id)
        {
            return _context.Partners.Any(e => e.PartnerId == id);
        }
    }
}
