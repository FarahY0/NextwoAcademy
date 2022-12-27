using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Nextwo.Data;
using Nextwo.Models;
using Nextwo.Models.ViewModel;

namespace Nextwo.Areas.Dashboard.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly AppDbContext _context;
        private readonly AppDbContext _dbContext;


        public HomeController( AppDbContext context, AppDbContext dbContext)
        {
           
            _context = context;
            _dbContext = dbContext;


        }
        [Area("Dashboard")]
        public async Task<IActionResult> Index()
        {
            //var appDbContext = _context.Courses.Where(x => x.OrderStatus == Models.Status.Wait);
            //return View(appDbContext.OrderByDescending(x => x.CreationDate));
            //var appDbContext = _context.Requests.Where(x => x.Status == Models.);
            //return View( appDbContext.OrderByDescending(x => x.CreationDate));
            var appDbContext = _context.Requests.Include(r => r.Course).Include(r => r.User);
            return View(await appDbContext.ToListAsync());


        }
        //public IActionResult Search()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Search(string SearchName)
        //{
        //    var result = _db.Courses.Where(a => a.CourseName.Contains(SearchName)
        //    || a.CourseDesc.Contains(SearchName)
        //    || a.Category.CategoryName.Contains(SearchName)).ToString();
        //     _db.Instructors.Where(a => a.InstructorName.Contains(SearchName)
        //     || a.InstructorPosition.Contains(SearchName)).ToString();
        //    return View(result);
        //}
        //[Area("Dashboard")]
        //public async Task<IActionResult> Accept(int? id)
        //{
        //    var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);
        //    course.OrderStatus = Models.Status.Yes;
        //    _context.Update(course);


        //    MimeMessage message = new MimeMessage();
        //    MailboxAddress from = new MailboxAddress("Nextwo", "faya2000yacoub@gmail.com");
        //    message.From.Add(from);
        //    MailboxAddress to = new MailboxAddress(course.CourseName, course.CourseDesc);
        //    message.To.Add(to);
        //    message.Subject = "Acceptance email";
        //    BodyBuilder builder = new BodyBuilder();
        //    builder.HtmlBody = "<h3>Congratulations!</h3>" + " <p>You have been accepted Your Course,you can now log in .<p>" + "<h5>Best Regards<h5>";
        //    message.Body = builder.ToMessageBody();

        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.gmail.com", 465, true);
        //        client.Authenticate("faya2000yacoub@gmail.com", "stddjqlwmvnhagrh");
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index", "Home");

        //}

        //[Area("Dashboard")]
        //public async Task<IActionResult> Reject(int? id)
        //{
        //    var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);
        //    course.OrderStatus = Models.Status.No;
        //    _context.Update(course);

        //    MimeMessage message = new MimeMessage();
        //    MailboxAddress from = new MailboxAddress("Nextwo", "faya2000yacoub@gmail.com");
        //    message.From.Add(from);
        //    MailboxAddress to = new MailboxAddress(course.CourseName, course.CourseDesc);
        //    message.To.Add(to);
        //    message.Subject = "Reject email";
        //    BodyBuilder builder = new BodyBuilder();
        //    builder.HtmlBody = "<h3>Reject!</h3>" + " <p>You have been Reject The Course,try again later .<p>" + "<h5>Best Regards<h5>";
        //    message.Body = builder.ToMessageBody();

        //    using (var client = new SmtpClient())
        //    {
        //       client.Connect("smtp.gmail.com", 465, true);
        //       client.Authenticate("faya2000yacoub@gmail.com", "stddjqlwmvnhagrh");
        //       client.Send(message);
        //        client.Disconnect(true);
        //    }
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index", "Home");
        //}

        //[Area("Dashboard")]
        //public async Task<IActionResult> Accept(Guid id)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
        //    user.OrderStatus = Models.StatUs.Yes;
        //    _context.Update(user);


        //    MimeMessage message = new MimeMessage();
        //    MailboxAddress from = new MailboxAddress("Nextwo", "faya2000yacoub@gmail.com");
        //    message.From.Add(from);
        //    MailboxAddress to = new MailboxAddress(user.UserName, user.Email);
        //    message.To.Add(to);
        //    message.Subject = "Acceptance email";
        //    BodyBuilder builder = new BodyBuilder();
        //    builder.HtmlBody = "<h3>Congratulations!</h3>" + " <p>You have been accepted Your Course,you can now log in .<p>" + "<h5>Best Regards<h5>";
        //    message.Body = builder.ToMessageBody();

        //    using (var clinte = new SmtpClient())
        //    {
        //        clinte.Connect("smtp.gmail.com", 465, true);
        //        clinte.Authenticate("faya2000yacoub@gmail.com", "stddjqlwmvnhagrh");
        //        clinte.Send(message);
        //        clinte.Disconnect(true);
        //    }
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index", "Home");

        //}

        //[Area("Dashboard")]
        //public async Task<IActionResult> Reject(Guid id)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
        //    user.OrderStatus = Models.StatUs.No;
        //    _context.Update(user);

        //    MimeMessage message = new MimeMessage();
        //    MailboxAddress from = new MailboxAddress("Hands Shop", "hebaalahmad48@gmail.com");
        //    message.From.Add(from);
        //    MailboxAddress to = new MailboxAddress(user.UserName, user.Email);
        //    message.To.Add(to);
        //    message.Subject = "Reject email";
        //    BodyBuilder builder = new BodyBuilder();
        //    builder.HtmlBody = "<h3>Reject!</h3>" + " <p>You have been Reject The Course,try again later .<p>" + "<h5>Best Regards<h5>";
        //    message.Body = builder.ToMessageBody();

        //    using (var clinte = new SmtpClient())
        //    {
        //        clinte.Connect("smtp.gmail.com", 465, true);
        //        clinte.Authenticate("faya2000yacoub@gmail.com", "stddjqlwmvnhagrh");
        //        clinte.Send(message);
        //        clinte.Disconnect(true);
        //    }
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index", "Home");
        //}






        //[HttpPost]
        //public async Task<IActionResult> SendRequest(CourseRequestViewModel viewModel)
        //{
        //    var adminEmail = "faya2000yacoub@gmail.com";
        //    var subject = "New Course Request";
        //    var message = $"A user has requested the course with ID {viewModel.CourseID}. Message: {viewModel.Message}";

        //    await _emailSender.SendEmailAsync(adminEmail, subject, message);

        //    return Ok(); 
        //}

        [HttpPost]
        public async Task<IActionResult> RequestEnrollment(Request request, int CourseId)
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
            using (var client = new System.Net.Mail.SmtpClient())
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

            using (var client = new System.Net.Mail.SmtpClient())
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

        //create reject  view


    }
}
