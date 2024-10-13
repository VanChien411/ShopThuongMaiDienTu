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
    }
}
