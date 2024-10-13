using Microsoft.AspNetCore.Mvc;
using ShopThuongMaiDienTu.Data;
using ShopThuongMaiDienTu.ViewModels;

namespace ShopThuongMaiDienTu.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ShopThuongMaiDienTuContext _context;

        public KhachHangController(ShopThuongMaiDienTuContext context) {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var khachHang = model;
            }
            return View();
        }
        public IActionResult DangNhap()
        {
            return View();
        }
    }
}
