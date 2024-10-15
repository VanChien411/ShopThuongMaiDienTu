using Microsoft.AspNetCore.Mvc;
using ShopThuongMaiDienTu.Data;
using ShopThuongMaiDienTu.ViewModels;
using ShopThuongMaiDienTu.Helpers;
using Microsoft.AspNetCore.Authorization;
using ShopThuongMaiDienTu.Models;

namespace ShopThuongMaiDienTu.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopThuongMaiDienTuContext _context;

        public CartController(ShopThuongMaiDienTuContext context)
        {
            _context = context;
        }
        
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();


        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHangHoa == id);
            if (item == null)
            {
                var hangHoa = _context.HangHoas.SingleOrDefault(x => x.MaHh == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Khong tim thay {id}";
                    return Redirect("/404");

                }
                item = new CartItem
                {
                    MaHangHoa = hangHoa.MaHh,
                    TenHangHoa = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = hangHoa.Hinh ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);

            }
            else
            {
                item.SoLuong += quantity;

            }
            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

            return RedirectToAction("index");
        }
        public IActionResult ChangeToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHangHoa == id);
            if (item == null)
            {
                var hangHoa = _context.HangHoas.SingleOrDefault(x => x.MaHh == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Khong tim thay {id}";
                    return Redirect("/404");

                }
                item = new CartItem
                {
                    MaHangHoa = hangHoa.MaHh,
                    TenHangHoa = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = hangHoa.Hinh ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);

            }
            else
            {
                item.SoLuong = quantity;

            }
            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

            return RedirectToAction("index");

        }
        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(x => x.MaHangHoa == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            }

            return RedirectToAction("index");
        }
        [Authorize]
        [HttpGet]
        public IActionResult CheckOut()
        {
           if(Cart.Count == 0)
            {
                return Redirect("/");
            }
            return View(Cart);
        }
        [Authorize]
        [HttpPost]
        public IActionResult CheckOut(CheckoutVM model)
        {
            if (ModelState.IsValid) {
                var customerId = HttpContext.User.Claims.SingleOrDefault(x => x.Type == MySetting.CLAIM_CUSTOMERID).Value;
                var khachHang = new KhachHang();
                if (model.GiongKhachHang)
                {
                    khachHang = _context.KhachHangs.SingleOrDefault(x => x.MaKh == customerId);
                }

                var hoadon = new HoaDon
                {
                    MaKh = customerId,
                    HoTen = model.HoTen ?? khachHang.HoTen,
                    DiaChi = model.DiaChi ?? khachHang.DiaChi,
                    NgayDat = DateTime.Now,
                    CachThanhToan = "COD",
                    CachVanChuyen = "Grab",
                    MaTrangThai = 0,
                    GhiChu = model.GhiChu
                };
                _context.Database.BeginTransaction();
                try
                {
                   
                    _context.Database.CommitTransaction();
                    _context.Add(hoadon);
                    _context.SaveChanges();

                    var cthd = new List<ChiTietHd>();
                    foreach(var item in Cart)
                    {
                        cthd.Add(new ChiTietHd
                        {
                            MaHd = hoadon.MaHd,
                            SoLuong = item.SoLuong,
                            DonGia = item.DonGia,
                            MaHh = item.MaHangHoa,
                            GiamGia = 0
                        });
                        _context.SaveChanges();
                        HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());

                        return View("Success");
                    }
                }
                catch
                {
                    _context.Database.RollbackTransaction();
                }
             
            }
            return View(Cart);
        }
    }
}
