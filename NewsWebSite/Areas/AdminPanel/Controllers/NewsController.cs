using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsWebSite.Models;
using System;
using System.IO;
using System.Linq;

namespace NewsWebSite.Controllers
{
    [Area("AdminPanel")]
    public class NewsController : Controller
    {
        private readonly NewsDbContext _context;
        private readonly IWebHostEnvironment _host;


        public NewsController(NewsDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: News
        public IActionResult Index()
        {
            var listNews = _context.News.Include(c => c.Category);
            return View(listNews.ToList());
        }

        // GET: News/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var detailNews = _context.News.Include(c => c.Category)
                                    .FirstOrDefault(m => m.Id == id);
            if (detailNews == null)
            {
                return NotFound();
            }

            return View(detailNews);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(New modelNews)
        {
            if (ModelState.IsValid)
            {
                UploadImage(modelNews);
                _context.Add(modelNews);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", modelNews.CategoryId);
            return View(modelNews);
        }

        void UploadImage(New model)
        {
            if (model.File != null)
            {
                string uploadFolder = Path.Combine(_host.WebRootPath, "Images/News");
                var uniqueFileName = Guid.NewGuid() + ".jpg"; //"_" + model.File;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.File.CopyTo(fileStream);
                }
                model.Image = uniqueFileName;
            }
        }

        // GET: News/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cnew = _context.News.Find(id);
            if (cnew == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", cnew.CategoryId);
            return View(cnew);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, New model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UploadImage(model);
                    _context.Update(model);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewExists(model.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
            return View(model);
        }

        // GET: News/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cnew = _context.News.Include(c => c.Category)
                .FirstOrDefault(m => m.Id == id);
            if (cnew == null)
            {
                return NotFound();
            }

            return View(cnew);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cnew = _context.News.Find(id);
            _context.News.Remove(cnew);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool NewExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
