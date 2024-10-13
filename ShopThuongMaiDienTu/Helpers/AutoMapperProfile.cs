using AutoMapper;
using ShopThuongMaiDienTu.Models;
using ShopThuongMaiDienTu.ViewModels;

namespace ShopThuongMaiDienTu.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() {
            CreateMap<RegisterVM, KhachHang>();
                //.ForMember(x => x.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
                //.ReverseMap();
                
        
        }
    }
}
