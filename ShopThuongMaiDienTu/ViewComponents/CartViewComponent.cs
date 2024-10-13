using Microsoft.AspNetCore.Mvc;
using ShopThuongMaiDienTu.Helpers;
using ShopThuongMaiDienTu.ViewModels;

namespace ShopThuongMaiDienTu.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>() ;
        return View("CartPannel", new CartVM
        {
            Quantity = cart.Sum(x => x.SoLuong),
            Total = cart.Sum(x => x.ThanhTien)
        }); 

        }
    
    }
}
