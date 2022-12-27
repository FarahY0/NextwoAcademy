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
    public class ArticlesController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _hostEnvironment;

        public ArticlesController(AppDbContext context , IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        // GET: Dashboard/Articles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Articles.ToListAsync());
        }

        // GET: Dashboard/Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Dashboard/Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleViewModel model)
        {

            if (ModelState.IsValid)
            {
                string ImgName = UploadeNewFile(model);
                Article article = new Article
                {
                    UserName=model.UserName,
                    ArticleTitle=model.ArticleTitle,
                    ArticleDesc=model.ArticleDesc,
                    PublishDate=model.PublishDate,
                    ArticleImg= ImgName
                };
                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Articles");
            }
            //if (ModelState.IsValid)
            //{
            //    _context.Add(article);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            return View(model);
        }
        public string UploadeNewFile(ArticleViewModel model)
        {
            string NewFullImgName = null;
            if (model.ArticleImg != null)
            {

                string fileRoot = Path.Combine(_hostEnvironment.WebRootPath, @"MyImages\");
                string newFileName = Guid.NewGuid() + "_" + model.ArticleImg.FileName;
                string fullpath = Path.Combine(fileRoot, newFileName);
                using (var myNewFile = new FileStream(fullpath, FileMode.Create))
                {
                    model.ArticleImg.CopyTo(myNewFile);
                }
                NewFullImgName = @"/MyImages/" + newFileName;
                return NewFullImgName;

            }
            return NewFullImgName;
        }

        // GET: Dashboard/Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Dashboard/Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,UserName,ArticleTitle,ArticleDesc,PublishDate,ArticleImg,CreationDate,IsDeleted,IsActive")] Article article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
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
            return View(article);
        }

        // GET: Dashboard/Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Dashboard/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}
