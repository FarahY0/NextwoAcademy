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
    public class InstructorsController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _hostEnvironment;
       
       
        public InstructorsController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
        _hostEnvironment = hostEnvironment;
    }

        // GET: Dashboard/Instructors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instructors.ToListAsync());
        }

        // GET: Dashboard/Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Dashboard/Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string ImgName = UploadeNewFile(model);
                Instructor instructor = new Instructor
                {
                  InstructorName=model.InstructorName,
                   InstructorPosition = model.InstructorPosition,
                    Twitter = model.Twitter,
                    Facebook = model.Facebook,
                    InstructorImg = ImgName

                };
                _context.Instructors.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Instructors");
            }
                return View(model);
        }
        public string UploadeNewFile(InstructorViewModel model)
        {
            string NewFullImgName = null;
            if (model.InstructorImg != null)
            {
                string fileRoot = Path.Combine(_hostEnvironment.WebRootPath, @"MyImages\");
                string newFileName = Guid.NewGuid() + "_" + model.InstructorImg.FileName;
                string fullpath = Path.Combine(fileRoot, newFileName);
                using (var myNewFile = new FileStream(fullpath, FileMode.Create))
                {
                    model.InstructorImg.CopyTo(myNewFile);
                }
                NewFullImgName = @"/MyImages/" + newFileName;
                return NewFullImgName;

            }
            return NewFullImgName;
        }

        // GET: Dashboard/Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: Dashboard/Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstructorId,InstructorName,InstructorImg,InstructorPosition,Twitter,Facebook,CreationDate,IsDeleted,IsActive")] Instructor instructor)
        {
            if (id != instructor.InstructorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.InstructorId))
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
            return View(instructor);
        }

        // GET: Dashboard/Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Dashboard/Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.InstructorId == id);
        }
    }
}
