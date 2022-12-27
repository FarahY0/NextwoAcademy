using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nextwo.Data;
using Microsoft.AspNetCore.Identity;
using Nextwo.Models.ViewModel;
using Nextwo.Models;
using Microsoft.AspNetCore.Authentication;

namespace Nextwo.Controllers
{
  
    public class HomeController : Controller 
    {
        private readonly AppDbContext _context;
        private readonly AppDbContext db;
        


        public HomeController(AppDbContext context , AppDbContext _db ) 
        {
            _context = context;
            db = _db;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        public async Task<IActionResult> Courses()
        {
            var appDbContext = _context.Courses.Include(c => c.Category);
            return View(await appDbContext.ToListAsync());
        }
        public async Task<IActionResult> Instructors()
        {
            return View(await _context.Instructors.ToListAsync());
        }
        public async Task<IActionResult> Articles()
        {
            return View(await _context.Articles.ToListAsync());
        }
       
        

        //[HttpPost]
        //public IActionResult Update(UserProfile model)
        //{
        //    // Update the user profile in the database
        //    _repository.UpdateUserProfile(model);

        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        [ActionName("LogOut")]
        public IActionResult LogOut()
        {
            // Sign out the user and clear the authentication cookie
            HttpContext.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

            // Redirect to the login page
            return RedirectToAction("Index", "Home");
        }
        //public async Task<IActionResult> UserProfile()
        //{
        //    var userProfile = await _context.UserProfiles
        //        .Include(u => u.Courses)
        //        .SingleOrDefaultAsync();

        //    if (userProfile == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userProfile);
        //}
        //[HttpGet]
        //public IActionResult AccountSettings()
        //{
        //    // Retrieve the user profile from the database
        //    var userProfile = _repository.GetUserProfile(User.Identity.Name);

        //    // Pass the user profile data to the view
        //    return View(userProfile);
        //}
        //[HttpPost]
        //public IActionResult UpdateAccountSettings(UserProfile model)
        //{
        //    // Update the user profile in the database
        //    _repository.UpdateUserProfile(model);

        //    return RedirectToAction("Index");
        //}







    }
}
