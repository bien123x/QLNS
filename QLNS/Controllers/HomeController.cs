using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QLNS.Data;
using QLNS.Models;

namespace QLNS.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbContext_App _context;

        public HomeController(DbContext_App context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.MaNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
            return RedirectToAction("Index", "ChamCongs");
        }
    }
}
