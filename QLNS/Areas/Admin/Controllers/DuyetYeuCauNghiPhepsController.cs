using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLNS.Data;

namespace QLNS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DuyetYeuCauNghiPhepsController : Controller
    {
        private readonly DbContext_App _context;

        public DuyetYeuCauNghiPhepsController(DbContext_App context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dbContext_App = _context.YeuCauNghiPheps.Include(y => y.NhanVien);
            return View(await dbContext_App.ToListAsync());
        }

        [HttpPost]
        public IActionResult CapNhatTinhTrang(int id, string tinhTrang)
        {
            var yeuCau = _context.YeuCauNghiPheps.Find(id);
            if (yeuCau == null)
            {
                return NotFound();
            }

            yeuCau.TinhTrang = tinhTrang;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Cập nhật trạng thái thành công!";
            return RedirectToAction("Index");
        }

    }
}
