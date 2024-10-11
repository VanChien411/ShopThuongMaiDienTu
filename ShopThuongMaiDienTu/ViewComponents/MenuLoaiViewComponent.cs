using Microsoft.AspNetCore.Mvc;
using ShopThuongMaiDienTu.Data;
using ShopThuongMaiDienTu.ViewModels;

namespace ShopThuongMaiDienTu.ViewComponents
{
    public class MenuLoaiViewComponent:ViewComponent
    {
        private readonly ShopThuongMaiDienTuContext _context;
        public MenuLoaiViewComponent(ShopThuongMaiDienTuContext context) { 
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var data = _context.Loais.Select(x => new MenuLoaiVM
            {
               MaLoai = x.MaLoai,
               TenLoai = x.TenLoai,
                SoLuong = x.HangHoas.Count()
            }).OrderBy(o => o.TenLoai);
            return View(data);
        }
    }
}
