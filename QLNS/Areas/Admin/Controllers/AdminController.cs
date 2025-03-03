using Microsoft.AspNetCore.Mvc;

namespace QLNS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // Kiểm tra nếu chưa đăng nhập
            if (HttpContext.Session.GetInt32("MaNguoiDung") == null)
            {
                ViewBag.CheckLogin = "Chưa đăng nhập!";
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            // Kiểm tra nếu không phải Admin
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Forbidden", "Admin");
            }

            return RedirectToAction("Index", "NhanViens");
        }


        public IActionResult Forbidden()
        {
            return View();
        }

    }
}
