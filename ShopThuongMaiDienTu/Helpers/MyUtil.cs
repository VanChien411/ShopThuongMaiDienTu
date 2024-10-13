using System.Text;

namespace ShopThuongMaiDienTu.Helpers
{
    public class MyUtil
    {
        public static string GenerateRandomKey(int lenght = 5)
        {
            var pattern = @"awdgrdgrhhggjh!dAawrWGDVSG";
            var sb = new StringBuilder();
            var rd = new Random();
            for(int i = 0; i < lenght; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }

        public static string UploadHinh(IFormFile Hinh, string folder)
        {
            try
            {
                var fullPath = Path.Combine(
              Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, Hinh.FileName
              );
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myfile);
                }
                return Hinh.FileName;
            }
            catch (Exception ex) { 
                return string.Empty;
            }
          
        }
    }
}
