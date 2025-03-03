using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLNS.Data;
using QLNS.Models;

namespace QLNS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ViTriCongViecsController : Controller
    {
        private readonly DbContext_App _context;

        public ViTriCongViecsController(DbContext_App context)
        {
            _context = context;
        }

        // GET: Admin/ViTriCongViecs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ViTriCongViecs.ToListAsync());
        }

        // GET: Admin/ViTriCongViecs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viTriCongViec = await _context.ViTriCongViecs
                .FirstOrDefaultAsync(m => m.MaViTri == id);
            if (viTriCongViec == null)
            {
                return NotFound();
            }

            return View(viTriCongViec);
        }

        // GET: Admin/ViTriCongViecs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ViTriCongViecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaViTri,TenViTri,HeSoLuong")] ViTriCongViec viTriCongViec)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viTriCongViec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viTriCongViec);
        }

        // GET: Admin/ViTriCongViecs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viTriCongViec = await _context.ViTriCongViecs.FindAsync(id);
            if (viTriCongViec == null)
            {
                return NotFound();
            }
            return View(viTriCongViec);
        }

        // POST: Admin/ViTriCongViecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaViTri,TenViTri,HeSoLuong")] ViTriCongViec viTriCongViec)
        {
            if (id != viTriCongViec.MaViTri)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viTriCongViec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViTriCongViecExists(viTriCongViec.MaViTri))
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
            return View(viTriCongViec);
        }

        // GET: Admin/ViTriCongViecs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viTriCongViec = await _context.ViTriCongViecs
                .FirstOrDefaultAsync(m => m.MaViTri == id);
            if (viTriCongViec == null)
            {
                return NotFound();
            }

            return View(viTriCongViec);
        }

        // POST: Admin/ViTriCongViecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viTriCongViec = await _context.ViTriCongViecs.FindAsync(id);
            if (viTriCongViec != null)
            {
                _context.ViTriCongViecs.Remove(viTriCongViec);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViTriCongViecExists(int id)
        {
            return _context.ViTriCongViecs.Any(e => e.MaViTri == id);
        }
    }
}
