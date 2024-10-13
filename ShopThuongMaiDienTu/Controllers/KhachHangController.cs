using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopThuongMaiDienTu.Data;
using ShopThuongMaiDienTu.Helpers;
using ShopThuongMaiDienTu.Models;
using ShopThuongMaiDienTu.ViewModels;

namespace ShopThuongMaiDienTu.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ShopThuongMaiDienTuContext _context;
        private readonly IMapper _mapper;

        public KhachHangController(ShopThuongMaiDienTuContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
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
                var khachHang = _mapper.Map<KhachHang>(model);
                khachHang.RandomKey = MyUtil.GenerateRandomKey();

            }
            return View();
        }
        public IActionResult DangNhap()
        {
            return View();
        }
    }
}
