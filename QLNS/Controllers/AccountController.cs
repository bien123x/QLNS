using Microsoft.AspNetCore.Mvc;
using QLNS.Data;

namespace QLNS.Controllers
{
    public class AccountController : Controller
    {
        private DbContext_App _context;
        public AccountController(DbContext_App context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string tenDangNhap, string matKhau)
        {
            var nguoiDung = _context.NguoiDungs
                .FirstOrDefault(x => x.TenDangNhap == tenDangNhap && x.MatKhau == matKhau);

            if (nguoiDung != null)
            {
                HttpContext.Session.SetInt32("MaNguoiDung", nguoiDung.MaNguoiDung);
                HttpContext.Session.SetString("TenDangNhap", nguoiDung.TenDangNhap);

                // Nếu tên đăng nhập là "admin", điều hướng đến trang Admin
                if (nguoiDung.TenDangNhap.ToLower() == "admin")
                {
                    HttpContext.Session.SetString("IsAdmin", "true"); // Lưu trạng thái Admin
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
                else
                {
                    HttpContext.Session.SetString("IsAdmin", "false");
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
