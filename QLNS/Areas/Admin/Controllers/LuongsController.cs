using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using QLNS.Data;
using QLNS.Models;

namespace QLNS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LuongsController : Controller
    {
        private readonly DbContext_App _context;

        public LuongsController(DbContext_App context)
        {
            _context = context;
        }

        // GET: Admin/Luongs
        public async Task<IActionResult> Index()
        {
            var dbContext_App = _context.Luongs.Include(l => l.NhanVien);
            return View(await dbContext_App.ToListAsync());
        }

        // GET: Admin/Luongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luong = await _context.Luongs
                .Include(l => l.NhanVien)
                .FirstOrDefaultAsync(m => m.MaLuong == id);
            if (luong == null)
            {
                return NotFound();
            }

            return View(luong);
        }

        // GET: Admin/Luongs/Create
        public IActionResult Create()
        {
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTenNV");
            return View();
        }

        // POST: Admin/Luongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLuong,MaNhanVien,Thang,Nam")] Luong luong)
        {
            var dsChamCong = _context.ChamCongs.Where(cc => cc.MaNhanVien == luong.MaNhanVien && cc.NgayLam.Date.Month == luong.Thang && cc.NgayLam.Date.Year == luong.Nam).ToList();
            if (dsChamCong.Count == 0) {
                ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTenNV", luong.MaNhanVien);
                ModelState.AddModelError("Thang", "Chưa có dữ liệu chấm công của tháng này!");
                return View();
            }
            var khoanTru = 0.0;
            var tangCa = 0.0;

            foreach (var chamCong in dsChamCong) {
                khoanTru += chamCong.ThieuGio;
                tangCa += chamCong.TangCa;
            }
            luong.KhoanTru = (decimal) khoanTru * 10000;
            luong.TienTangCa = (decimal) tangCa * 10000;

            var viTri = _context.NhanViens.FirstOrDefault(nv => nv.MaNhanVien == luong.MaNhanVien).MaViTri;
            var heSoLuong = _context.ViTriCongViecs.Find(viTri).HeSoLuong;

            var soNgayChamCong = _context.ChamCongs.Count(cc => cc.MaNhanVien == luong.MaNhanVien && cc.NgayLam.Date.Month == luong.Thang && cc.NgayLam.Date.Year == luong.Nam);
            var soNgayNghi = 0;

            foreach (var yc in _context.YeuCauNghiPheps.Where(yc => yc.MaNhanVien == luong.MaNhanVien && yc.NgayBatDau.Month == luong.Thang && yc.NgayBatDau.Year == luong.Nam).ToList())
            {
                if (yc.TinhTrang == "Đã Duyệt")
                {
                    soNgayNghi += yc.TongSoNgay;
                }
            }

            luong.LuongThucNhan = (decimal)(heSoLuong * 2500000 * (soNgayChamCong + soNgayNghi) / 26) + luong.TienTangCa + luong.KhoanTru;

            if (ModelState.IsValid)
            {
                _context.Add(luong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTenNV", luong.MaNhanVien);
            return View(luong);
        }

        // GET: Admin/Luongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luong = await _context.Luongs.FindAsync(id);
            if (luong == null)
            {
                return NotFound();
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "GioiTinh", luong.MaNhanVien);
            return View(luong);
        }

        // POST: Admin/Luongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLuong,MaNhanVien,Thang,Nam,TienTangCa,KhoanTru,LuongThucNhan")] Luong luong)
        {
            if (id != luong.MaLuong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(luong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LuongExists(luong.MaLuong))
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
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "GioiTinh", luong.MaNhanVien);
            return View(luong);
        }

        // GET: Admin/Luongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luong = await _context.Luongs
                .Include(l => l.NhanVien)
                .FirstOrDefaultAsync(m => m.MaLuong == id);
            if (luong == null)
            {
                return NotFound();
            }

            return View(luong);
        }

        // POST: Admin/Luongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var luong = await _context.Luongs.FindAsync(id);
            if (luong != null)
            {
                _context.Luongs.Remove(luong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LuongExists(int id)
        {
            return _context.Luongs.Any(e => e.MaLuong == id);
        }

        public IActionResult ExportExcel(int thang, int nam)
        {
            var danhSachLuong = _context.Luongs
                .Where(l => l.Thang == thang && l.Nam == nam)
                .Select(l => new
                {
                    HoTen = l.NhanVien.HoTenNV,
                    Thang = l.Thang,
                    Nam = l.Nam,
                    TienTangCa = l.TienTangCa,
                    KhoanTru = l.KhoanTru,
                    LuongThucNhan = l.LuongThucNhan
                })
                .ToList();

            if (!danhSachLuong.Any())
            {
                return Content("Không có dữ liệu để xuất!");
            }

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Bảng Lương");

                // Header
                worksheet.Cells[1, 1].Value = "Nhân Viên";
                worksheet.Cells[1, 2].Value = "Tháng";
                worksheet.Cells[1, 3].Value = "Năm";
                worksheet.Cells[1, 4].Value = "Tiền Tăng Ca";
                worksheet.Cells[1, 5].Value = "Khoản Trừ";
                worksheet.Cells[1, 6].Value = "Lương Thực Nhận";

                int row = 2;
                foreach (var luong in danhSachLuong)
                {
                    worksheet.Cells[row, 1].Value = luong.HoTen;
                    worksheet.Cells[row, 2].Value = luong.Thang;
                    worksheet.Cells[row, 3].Value = luong.Nam;
                    worksheet.Cells[row, 4].Value = luong.TienTangCa;
                    worksheet.Cells[row, 5].Value = luong.KhoanTru;
                    worksheet.Cells[row, 6].Value = luong.LuongThucNhan;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string fileName = $"BangLuong_{thang}_{nam}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

    }
}
