using Microsoft.AspNetCore.Mvc;
using ShopThuongMaiDienTu.Data;
using ShopThuongMaiDienTu.ViewModels;

namespace ShopThuongMaiDienTu.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly ShopThuongMaiDienTuContext _context;

        public HangHoaController(ShopThuongMaiDienTuContext context) { 
            _context = context;
        }
        public IActionResult Index(int? loai)
        {
            var hangHoas = _context.HangHoas.AsQueryable();

            if (loai.HasValue) { 
                hangHoas = hangHoas.Where(x => x.MaLoai == loai.Value);
            }

            var result = hangHoas.Select(x => new HangHoaVM
            {
                MaHangHoa = x.MaHh,
                TenHangHoa = x.TenHh,
                DonGia = x.DonGia ?? 0,
                Hinh = x.Hinh ?? "",
                MoTaNgan = x.MoTaDonVi ?? "",
                TenLoai = x.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
    }
}
