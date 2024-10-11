using Microsoft.AspNetCore.Mvc;

namespace ShopThuongMaiDienTu.Controllers
{
    public class HangHoaController : Controller
    {
        public IActionResult Index(int? loai)
        {
            return View();
        }
    }
}
