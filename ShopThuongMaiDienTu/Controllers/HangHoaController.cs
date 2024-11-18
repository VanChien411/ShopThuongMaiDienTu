using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        public IActionResult Index(int? loai, int page = 1, int pageSize = 6)
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
            // Tính tổng số trang
            int totalItems = result.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            // Lấy dữ liệu của trang hiện tại
            var itemsOnPage = result
                .Skip((page - 1) * pageSize) // Bỏ qua các phần tử của các trang trước
                .Take(pageSize)             // Lấy số lượng phần tử của trang hiện tại
                .ToList();

            // Truyền dữ liệu đến View
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            return View(itemsOnPage);
        }

        public IActionResult Search(string? Name)
        {
            var hangHoas = _context.HangHoas.AsQueryable();

            if (!string.IsNullOrEmpty(Name))
            {
                hangHoas = hangHoas.Where(x => x.TenHh.ToLower().Contains(Name.ToLower()));
            }

            // Lưu giá trị của Name vào ViewBag
            ViewBag.SearchName = Name;

            var result = hangHoas.Select(x => new HangHoaVM
            {
                MaHangHoa = x.MaHh,
                TenHangHoa = x.TenHh,
                DonGia = x.DonGia ?? 0,
                Hinh = x.Hinh ?? "",
                MoTaNgan = x.MoTaDonVi ?? "",
                TenLoai = x.MaLoaiNavigation.TenLoai,
                
            });
            return View("Index",result);
        }
        public IActionResult Detail(int id)
        {
            var data = _context.HangHoas.Include(o => o.MaLoaiNavigation).SingleOrDefault(x => x.MaHh == id);
            if (data == null)
            {
                TempData["Message"] = $"Không tìm thấy sản phẩm {id}";
                return Redirect("/404");
            }

            var result = new ChiTietHangHoaVM
            {
                MaHangHoa = data.MaHh,
                TenHangHoa = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty,
                DiemDanhGia = 4,
                Hinh = data.Hinh ?? string.Empty,
                MoTaNgan = data.MoTaDonVi ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10,
            };
            return View(result);
        }
      
    }
}
