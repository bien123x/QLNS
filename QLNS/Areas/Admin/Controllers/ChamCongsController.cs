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
    public class ChamCongsController : Controller
    {
        private readonly DbContext_App _context;

        public ChamCongsController(DbContext_App context)
        {
            _context = context;
        }

        // GET: Admin/ChamCongs
        public async Task<IActionResult> Index()
        {
            var dbContext_App = _context.ChamCongs.Include(c => c.NhanVien);
            return View(await dbContext_App.ToListAsync());
        }

        // GET: Admin/ChamCongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamCong = await _context.ChamCongs
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(m => m.MaChamCong == id);
            if (chamCong == null)
            {
                return NotFound();
            }

            return View(chamCong);
        }

        // GET: Admin/ChamCongs/Create
        public IActionResult Create()
        {
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "GioiTinh");
            
            return View();
        }

        // POST: Admin/ChamCongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaChamCong,MaNhanVien,NgayLam,GioVaoLam,GioKetThuc")] ChamCong chamCong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chamCong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "GioiTinh", chamCong.MaNhanVien);
            return View(chamCong);
        }

        // GET: Admin/ChamCongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamCong = await _context.ChamCongs.FindAsync(id);
            if (chamCong == null)
            {
                return NotFound();
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "GioiTinh", chamCong.MaNhanVien);
            return View(chamCong);
        }

        // POST: Admin/ChamCongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaChamCong,MaNhanVien,NgayLam,GioVaoLam,GioKetThuc")] ChamCong chamCong)
        {
            if (id != chamCong.MaChamCong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chamCong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChamCongExists(chamCong.MaChamCong))
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
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "GioiTinh", chamCong.MaNhanVien);
            return View(chamCong);
        }

        // GET: Admin/ChamCongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamCong = await _context.ChamCongs
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(m => m.MaChamCong == id);
            if (chamCong == null)
            {
                return NotFound();
            }

            return View(chamCong);
        }

        // POST: Admin/ChamCongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chamCong = await _context.ChamCongs.FindAsync(id);
            if (chamCong != null)
            {
                _context.ChamCongs.Remove(chamCong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChamCongExists(int id)
        {
            return _context.ChamCongs.Any(e => e.MaChamCong == id);
        }

        public int getMaNhanVien()
        {
            var maND = HttpContext.Session.GetInt32("MaNguoiDung");
            var maNV = _context.NhanViens.FirstOrDefault(nv => nv.MaNguoiDung == maND).MaNhanVien;
            return maNV;
        }
    }
}
