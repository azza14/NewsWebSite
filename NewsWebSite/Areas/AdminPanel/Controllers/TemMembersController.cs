using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsWebSite.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebSite.Controllers
{
    [Area("AdminPanel")]
    public class TemMembersController : Controller
    {
        private readonly NewsDbContext _context;
        private readonly IWebHostEnvironment _host;

        public TemMembersController(NewsDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: TemMembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.TemMembers.ToListAsync());
        }

        // GET: TemMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temMember = await _context.TemMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (temMember == null)
            {
                return NotFound();
            }

            return View(temMember);
        }

        // GET: TemMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TemMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TemMember model)
        {
            if (ModelState.IsValid)
            {
                UploadImage(model);
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: TemMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temMember = await _context.TemMembers.FindAsync(id);
            if (temMember == null)
            {
                return NotFound();
            }
            return View(temMember);
        }

        // POST: TemMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TemMember temMember)
        {
            if (id != temMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UploadImage(temMember);
                    _context.Update(temMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemMemberExists(temMember.Id))
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
            return View(temMember);
        }

        // GET: TemMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temMember = await _context.TemMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (temMember == null)
            {
                return NotFound();
            }

            return View(temMember);
        }

        // POST: TemMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var temMember = await _context.TemMembers.FindAsync(id);
            _context.TemMembers.Remove(temMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemMemberExists(int id)
        {
            return _context.TemMembers.Any(e => e.Id == id);
        }

        void UploadImage(TemMember model)
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

    }
}
