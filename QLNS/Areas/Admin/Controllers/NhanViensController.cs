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
    public class NhanViensController : Controller
    {
        private readonly DbContext_App _context;

        public NhanViensController(DbContext_App context)
        {
            _context = context;
        }

        // GET: Admin/NhanViens
        public async Task<IActionResult> Index()
        {
            var dbContext_App = _context.NhanViens.Include(n => n.NguoiDung).Include(n => n.PhongBan).Include(n => n.ViTriCongViec);
            return View(await dbContext_App.ToListAsync());
        }

        // GET: Admin/NhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.NguoiDung)
                .Include(n => n.PhongBan)
                .Include(n => n.ViTriCongViec)
                .FirstOrDefaultAsync(m => m.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Create
        public IActionResult Create()
        {
            var nguoiDungs = _context.NguoiDungs.Where(nd => !_context.NhanViens.Any(nv => nv.MaNguoiDung == nd.MaNguoiDung)).ToList();

            ViewData["MaNguoiDung"] = new SelectList(nguoiDungs, "MaNguoiDung", "TenDangNhap");
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan");
            ViewData["MaViTri"] = new SelectList(_context.ViTriCongViecs, "MaViTri", "TenViTri");
            return View();
        }

        // POST: Admin/NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNhanVien,HoTenNV,NgaySinh,GioiTinh,DiaChi,SoDT,NgayVaoLam,Email,MaNguoiDung,MaPhongBan,MaViTri")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "TenDangNhap", nhanVien.MaNguoiDung);
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
            ViewData["MaViTri"] = new SelectList(_context.ViTriCongViecs, "MaViTri", "TenViTri", nhanVien.MaViTri);
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "MatKhau", nhanVien.MaNguoiDung);
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
            ViewData["MaViTri"] = new SelectList(_context.ViTriCongViecs, "MaViTri", "TenViTri", nhanVien.MaViTri);
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNhanVien,HoTenNV,NgaySinh,GioiTinh,DiaChi,SoDT,NgayVaoLam,Email,MaNguoiDung,MaPhongBan,MaViTri")] NhanVien nhanVien)
        {
            if (id != nhanVien.MaNhanVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.MaNhanVien))
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
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "MatKhau", nhanVien.MaNguoiDung);
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
            ViewData["MaViTri"] = new SelectList(_context.ViTriCongViecs, "MaViTri", "TenViTri", nhanVien.MaViTri);
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.NguoiDung)
                .Include(n => n.PhongBan)
                .Include(n => n.ViTriCongViec)
                .FirstOrDefaultAsync(m => m.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien != null)
            {
                _context.NhanViens.Remove(nhanVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
            return _context.NhanViens.Any(e => e.MaNhanVien == id);
        }
    }
}
