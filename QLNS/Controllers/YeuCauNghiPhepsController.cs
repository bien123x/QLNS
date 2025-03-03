using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLNS.Data;
using QLNS.Models;

namespace QLNS.Controllers
{
    public class YeuCauNghiPhepsController : Controller
    {
        private readonly DbContext_App _context;

        public YeuCauNghiPhepsController(DbContext_App context)
        {
            _context = context;
        }

        // GET: YeuCauNghiPheps
        public async Task<IActionResult> Index()
        {
            var maNV = getMaNhanVien();

            var dbContext_App = _context.YeuCauNghiPheps.Include(y => y.NhanVien).Where(yc => yc.MaNhanVien == maNV);
            return View(await dbContext_App.ToListAsync());
        }

        // GET: YeuCauNghiPheps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yeuCauNghiPhep = await _context.YeuCauNghiPheps
                .Include(y => y.NhanVien)
                .FirstOrDefaultAsync(m => m.MaNghiPhep == id);
            if (yeuCauNghiPhep == null)
            {
                return NotFound();
            }

            return View(yeuCauNghiPhep);
        }

        // GET: YeuCauNghiPheps/Create
        public IActionResult Create()
        {
            ViewData["MaNhanVien"] = getMaNhanVien();
            return View();
        }

        // POST: YeuCauNghiPheps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNghiPhep,MaNhanVien,NgayBatDau,NgayKetThuc,TongSoNgay,LyDo,TinhTrang")] YeuCauNghiPhep yeuCauNghiPhep)
        {
            if (yeuCauNghiPhep.NgayBatDau > yeuCauNghiPhep.NgayKetThuc)
            {
                ViewData["MaNhanVien"] = getMaNhanVien();
                ModelState.AddModelError("NgayBatDau", "Ngày bắt đầu không được vượt ngày kết thúc!");
                return View(yeuCauNghiPhep);
            }

            var ngayChamCong = _context.ChamCongs
                .Where(c => c.MaNhanVien == getMaNhanVien())
                .Select(c => c.NgayLam)
                .ToList();

            var ngayXinNghi = Enumerable.Range(0, (yeuCauNghiPhep.NgayKetThuc - yeuCauNghiPhep.NgayBatDau).Days + 1)
                                .Select(offset => yeuCauNghiPhep.NgayBatDau.AddDays(offset));

            if (ngayXinNghi.Any(ngay => ngayChamCong.Contains(ngay)))
            {
                ModelState.AddModelError("NgayBatDau", "Ngày nghỉ trùng với ngày đã đi làm.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(yeuCauNghiPhep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNhanVien"] = getMaNhanVien();
            return View(yeuCauNghiPhep);
        }

        // GET: YeuCauNghiPheps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yeuCauNghiPhep = await _context.YeuCauNghiPheps.FindAsync(id);
            if (yeuCauNghiPhep == null)
            {
                return NotFound();
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "GioiTinh", yeuCauNghiPhep.MaNhanVien);
            return View(yeuCauNghiPhep);
        }

        // POST: YeuCauNghiPheps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNghiPhep,MaNhanVien,NgayBatDau,NgayKetThuc,TongSoNgay,LyDo,TinhTrang")] YeuCauNghiPhep yeuCauNghiPhep)
        {
            if (id != yeuCauNghiPhep.MaNghiPhep)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yeuCauNghiPhep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YeuCauNghiPhepExists(yeuCauNghiPhep.MaNghiPhep))
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
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "GioiTinh", yeuCauNghiPhep.MaNhanVien);
            return View(yeuCauNghiPhep);
        }

        // GET: YeuCauNghiPheps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yeuCauNghiPhep = await _context.YeuCauNghiPheps
                .Include(y => y.NhanVien)
                .FirstOrDefaultAsync(m => m.MaNghiPhep == id);
            if (yeuCauNghiPhep == null)
            {
                return NotFound();
            }

            return View(yeuCauNghiPhep);
        }

        // POST: YeuCauNghiPheps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yeuCauNghiPhep = await _context.YeuCauNghiPheps.FindAsync(id);
            if (yeuCauNghiPhep != null)
            {
                _context.YeuCauNghiPheps.Remove(yeuCauNghiPhep);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YeuCauNghiPhepExists(int id)
        {
            return _context.YeuCauNghiPheps.Any(e => e.MaNghiPhep == id);
        }
        public int getMaNhanVien()
        {
            var maND = HttpContext.Session.GetInt32("MaNguoiDung");
            var maNV = _context.NhanViens.FirstOrDefault(nv => nv.MaNguoiDung == maND).MaNhanVien;
            return maNV;
        }
    }
}
