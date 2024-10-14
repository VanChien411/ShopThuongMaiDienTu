using System.ComponentModel.DataAnnotations;

namespace ShopThuongMaiDienTu.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage ="Nhập tên đăng nhập")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 ky tu")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Nhập tên mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
