using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nextwo.Data;
using Nextwo.Models;
using Nextwo.Models.ViewModel;

namespace Nextwo.Controllers
{
    public class AccountController : Controller
    {
        public AppDbContext db;
        private IWebHostEnvironment _hostEnvironment;


        public AccountController(AppDbContext _db , IWebHostEnvironment hostEnvironment)
        {
            
            db = _db;
        _hostEnvironment= hostEnvironment;
        }
        public IActionResult Register()
        {
            ViewBag.Roles = new SelectList(db.Roles, "RoleId", "RoleName");
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Address = model.Address,
                    CreationDate = DateTime.Now,
                    Email = model.Email,
                    RoleId = model.RoleId,
                    Role=model.Role,
                    IsActive = true,
                    IsDeleted = false,
                    Mobile = "0796671206",
                    Password = model.Password,
                    UserName = model.UserName
                };
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("LogIn");
            }
            return View(model);
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = db.Users.Where(x => x.UserName == model.UserName && x.Password == model.Password );
                if (data.Any( ))
                {
                    HttpContext.Session.SetString("Name", model.UserName);
                    var data1 = db.Users.Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefault();
                    var x = db.Roles.FirstOrDefault(m => m.RoleId.ToString() == data1.RoleId.ToString());
                    if (x.RoleName == "User")
                    {
                        return RedirectToAction("Index", "Home", new { area = "UserDashboard" });
                    }
                    else if(x.RoleName == "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Dashboard" });
                    }
                }
                ViewBag.error = "Invalid Password Or UserName";
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult MyAccout()
        {
            var name = HttpContext.Session.GetString("Name").ToString();
            var OfId = db.Users.FirstOrDefault(n => n.UserName == name);
            MyAccountViewModel myAccount = new MyAccountViewModel
            {
                UserName = OfId.UserName,
                Email = OfId.Email,
                ImgString = OfId.UserImg,
                Password = OfId.Password,

            };
            return View(myAccount);
        }

        public string UploadNewFile(MyAccountViewModel sub)
        {
            string newFullImgName = null;
            if (sub.UserImg != null)
            {

                string fileRoot = Path.Combine(_hostEnvironment.WebRootPath, @"MyImages\");
                string newFilName = Guid.NewGuid() + "_" + sub.UserImg.FileName;
                string fullPath = Path.Combine(fileRoot, newFilName);
                using (var myNewFile = new FileStream(fullPath, FileMode.Create))
                {
                    sub.UserImg.CopyTo(myNewFile);
                }

                newFullImgName = @"/MyImages/" + newFilName;
                return newFullImgName;
            }


            return newFullImgName;

        }


        [HttpPost]
        public IActionResult MyAccout(MyAccountViewModel myAccount)
        {
            string imgName = UploadNewFile(myAccount);
            var name = HttpContext.Session.GetString("Name").ToString();
            var OfId = db.Users.FirstOrDefault(n => n.UserName == name);


            if (myAccount.UserName == OfId.UserName)
            {
                OfId.UserName = myAccount.UserName;

            }
            else
            {
                OfId.UserName = myAccount.UserName;
                HttpContext.Session.SetString("Name", myAccount.UserName);
            }

            OfId.Email = myAccount.Email;
            if (myAccount.Password == null)
            {
                OfId.Password = OfId.Password;
            }

            else
            {
                OfId.Password = myAccount.Password;

            }

            if (myAccount.UserImg == null)
            {
                OfId.UserImg = myAccount.ImgString;
            }
            else
            {
                OfId.UserImg = imgName;
            }

            db.Update(OfId);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");


        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn");
        }

        public new IActionResult Request(int courseID)
        {
            var viewModel = new CourseRequestViewModel
            {
                CourseID = courseID
            };

            return View(viewModel);
        }




        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        var user = await userManager.GetAsync(User);
        //        if (user == null)
        //        {
        //            return RedirectToAction("LogIn");
        //        }
        //        var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        //        if(!result.Succeeded)
        //        {
        //            foreach(var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //            return View();
        //        }
        //        await SignInManager.RefreshSignInAsync(user);
        //        return View("ChangePasswordConfirmation");


        //        };
        //    return View(model);
        //    }



    }

}
