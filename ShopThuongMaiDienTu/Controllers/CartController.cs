﻿using Microsoft.AspNetCore.Mvc;
using ShopThuongMaiDienTu.Data;
using ShopThuongMaiDienTu.ViewModels;
using ShopThuongMaiDienTu.Helpers;

namespace ShopThuongMaiDienTu.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopThuongMaiDienTuContext _context;

        public CartController(ShopThuongMaiDienTuContext context)
        {
            _context = context;
        }
        const string CART_KEY = "MYCART";
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();


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
            HttpContext.Session.Set(CART_KEY, gioHang);

            return RedirectToAction("index");
        }
        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(x => x.MaHangHoa == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(CART_KEY, gioHang);
            }

            return RedirectToAction("index");
        }
    }
}