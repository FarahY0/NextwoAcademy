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
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _hostEnvironment;

        public UsersController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        // GET: Dashboard/Users
        public  IActionResult Index()
        {
            //var appDbContext = _context.Users.Include(u => u.Role);
            //return View(await appDbContext.ToListAsync());
            var appDbContext = _context.Users.Include(u => u.Role).Where(x => (x.OrderStatus == Models.StatUs.Yes || x.OrderStatus == Models.StatUs.Null));
            return View(appDbContext.OrderByDescending(x => x.CreationDate));
        }

        // GET: Dashboard/Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Dashboard/Users/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: Dashboard/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {

            if (ModelState.IsValid)
            {
                string ImgName = UploadeNewFile(model);
                User user = new User
                {
                    UserName = model.UserName,
                    Password=model.Password,
                    UserImg = ImgName,
                    Address =model.Address,
                    Email =model.Email,
                    Mobile=model.Mobile,
                    OrderStatus= model.OrderStatus,
                    RoleId=model.RoleId,
                    Courses = model.Courses
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Users");
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", model.RoleId);
            return View(model);
        }
        public string UploadeNewFile(UserViewModel model)
        {
            string NewFullImgName = null;
            if (model.UserImg != null)
            {

                string fileRoot = Path.Combine(_hostEnvironment.WebRootPath, @"MyImages\");
                string newFileName = Guid.NewGuid() + "_" + model.UserImg.FileName;
                string fullpath = Path.Combine(fileRoot, newFileName);
                using (var myNewFile = new FileStream(fullpath, FileMode.Create))
                {
                    model.UserImg.CopyTo(myNewFile);
                }
                NewFullImgName = @"/MyImages/" + newFileName;
                return NewFullImgName;

            }
            return NewFullImgName;
        }

        // GET: Dashboard/Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: Dashboard/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,UserName,Password,Mobile,Email,Address,UserImg,Gender,OrderStatus,RoleId,CreationDate,IsDeleted,IsActive")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: Dashboard/Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Dashboard/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
