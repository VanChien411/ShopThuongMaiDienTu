using System.ComponentModel.DataAnnotations;

namespace ShopThuongMaiDienTu.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage ="*")]
        [MaxLength(20, ErrorMessage ="Tối đa 20 ký tự")]
        public string MaKh { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage ="*")]
        public string MatKhau { get; set; }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage ="Tối đa 50 ký tự")]
        public string HoTen { get; set; }

        public bool? GioiTinh { get; set; } = true;

        public DateTime? NgaySinh { get; set; }
        [MaxLength(70, ErrorMessage = "Tối đa 70 ký tự")]
        public string? DiaChi { get; set; }
        [MaxLength(30, ErrorMessage = "Tối đa 30 ký tự")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage ="Chưa đúng định dạng điện thoại")]
        public string? DienThoai { get; set; }
        [EmailAddress(ErrorMessage ="Chưa đứng định dạng email")]
        public string Email { get; set; }

        public string? Hinh { get; set; }

   
    }
}
