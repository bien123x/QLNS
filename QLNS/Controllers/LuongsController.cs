using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLNS.Data;

namespace QLNS.Controllers
{
    public class LuongsController : Controller
    {
        private DbContext_App _context;
        public LuongsController(DbContext_App context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var luongs = _context.Luongs.Include(l => l.NhanVien).Where(l => l.MaNhanVien == getMaNhanVien()).ToList();
            return View(luongs);
        }

        [HttpPost]
        public IActionResult Index(int month, int year)
        {
            var luongs = _context.Luongs.Include(l => l.NhanVien).Where(l => l.MaNhanVien == getMaNhanVien() && l.Thang == month && l.Nam == year).ToList();
            return View(luongs);
        }
        public int getMaNhanVien()
        {
            var maND = HttpContext.Session.GetInt32("MaNguoiDung");
            var maNV = _context.NhanViens.FirstOrDefault(nv => nv.MaNguoiDung == maND).MaNhanVien;
            return maNV;
        }
    }
}
