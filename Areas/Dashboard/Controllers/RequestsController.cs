using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nextwo.Data;
using Nextwo.Models;

namespace Nextwo.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class RequestsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly AppDbContext _dbContext;

        public RequestsController(AppDbContext context , AppDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

            [HttpPost]
            public async Task<IActionResult> RequestEnrollment(Request request ,int CourseId)
            {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to sign-up page
                return RedirectToAction("Register", "Account");
            }
            // Save the request to the database.
            _dbContext.Requests.Add(request);
                await _dbContext.SaveChangesAsync();

                // Send an email to the admin with the request information.
                using (var client = new SmtpClient())
                {
                    var message = new MailMessage
                    {
                        Subject = "New Course Enrollment Request",
                        Body = $"{request.NameUser} ({request.Email}) has requested to enroll in the {request.Course} course.",
                        From = new MailAddress(request.Email),
                        To = { "faya2000yacoub@gmail.com" }
                    };
                    await client.SendMailAsync(message);
                }

                return RedirectToAction("EnrollmentRequestSuccess");
            }
        public async Task<IActionResult> PendingRequests()
        {
            var requests = await _dbContext.Requests
                .Where(r => r.Status == "Pending")
                .ToListAsync();
            return View(requests);
        }
        [HttpPost]
        public async Task<IActionResult> AcceptRequest(int requestId)
        {
            var request = await _dbContext.Requests.FindAsync(requestId);
            if (request == null)
            {
                return NotFound();
            }

            request.Status = "Accepted";
            await _dbContext.SaveChangesAsync();

            using (var client = new SmtpClient())
            {
                var message = new MailMessage
                {
                    Subject = "Course Enrollment Request Accepted",
                    Body = $"Your request to enroll in the {request.Course} course has been accepted.",
                    From = new MailAddress("faya2000yacoub@gmail.com"),
                    To = { request.Email }
                };
                await client.SendMailAsync(message);
            }

            return RedirectToAction("PendingRequests");
        }





        // GET: Dashboard/Requests
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Requests.Include(r => r.Course).Include(r => r.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Dashboard/Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Course)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Dashboard/Requests/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseDesc");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Address");
            return View();
        }

        // POST: Dashboard/Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,IsAccept,UserId,CourseId,CreationDate,IsDeleted,IsActive")] Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseDesc", request.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Address", request.UserId);
            return View(request);
        }

        // GET: Dashboard/Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseDesc", request.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Address", request.UserId);
            return View(request);
        }

        // POST: Dashboard/Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,IsAccept,UserId,CourseId,CreationDate,IsDeleted,IsActive")] Request request)
        {
            if (id != request.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.RequestId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseDesc", request.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Address", request.UserId);
            return View(request);
        }

        // GET: Dashboard/Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Course)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Dashboard/Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.RequestId == id);
        }
    }
}
