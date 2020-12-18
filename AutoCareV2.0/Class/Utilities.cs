using System.Security.Cryptography;
using System.Text;

namespace AutoCareV2._0.Class
{
    public class StringUtil
    {
        private static readonly string[] VietnameseSigns = new string[]
        {
              "aAeEoOuUiIdDyY",
              "áàạảãâấầậẩẫăắằặẳẵ",
              "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
              "éèẹẻẽêếềệểễ",
              "ÉÈẸẺẼÊẾỀỆỂỄ",
              "óòọỏõôốồộổỗơớờợởỡ",
              "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
              "úùụủũưứừựửữ",
              "ÚÙỤỦŨƯỨỪỰỬỮ",
              "íìịỉĩ",
              "ÍÌỊỈĨ",
              "đ",
              "Đ",
              "ýỳỵỷỹ",
              "ÝỲỴỶỸ"
        };

        public static string RemoveSign4VietnameseString(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return RemoveSign4VietnameseUnicodeComposite(str);
        }

        private static string RemoveSign4VietnameseUnicodeComposite(string str)
        {
            str = str
                .Replace(((char)769).ToString(), "") //dấu sắc
                .Replace(((char)768).ToString(), "") //dấu huyền
                .Replace(((char)777).ToString(), "") //dấu hỏi
                .Replace(((char)771).ToString(), "") //dấu ngã
                .Replace(((char)803).ToString(), ""); //dấu nặng

            return str;
        }
    }

    internal class Utilities
    {
        public static string GetMd5Hash(string input, string key)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] binput = Encoding.UTF8.GetBytes(input);
                byte[] bkey = Encoding.UTF8.GetBytes(key);
                md5Hash.TransformBlock(binput, 0, binput.Length, binput, 0);
                md5Hash.TransformFinalBlock(bkey, 0, bkey.Length);

                byte[] data = md5Hash.Hash;
                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();
                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        public static string smsreplace_TuyBien(string sms, string tenkh, string ngaysinh, string thuonghieu, string sodienthoai)
        {
            sms = sms.Replace("[TenKH]", tenkh);
            sms = sms.Replace("[NgaySinh]", ngaysinh);
            sms = sms.Replace("[ThuongHieu]", thuonghieu);
            sms = sms.Replace("[SoDienThoai]", sodienthoai);
            return sms;
        }

        public static string Smsreplace(string sms, string tenkh, string ngaysinh, string thuonghieu, string tenxe, string bienso, string sokhung, string somay, string sodienthoai, string solanbd, string ngaybd)
        {
            sms = sms.Replace("[TenKH]", tenkh);
            sms = sms.Replace("[NgaySinh]", ngaysinh);
            sms = sms.Replace("[ThuongHieu]", thuonghieu);
            sms = sms.Replace("[Tenxe]", tenxe);
            sms = sms.Replace("[BienSo]", bienso);
            sms = sms.Replace("[SoKhung]", sokhung);
            sms = sms.Replace("[SoMay]", somay);
            sms = sms.Replace("[SoDienThoai]", sodienthoai);
            sms = sms.Replace("[SolanBD]", solanbd);
            sms = sms.Replace("[NgayBaoDuong]", ngaybd);
            sms = sms.Replace("[TuNgay]", "");
            sms = sms.Replace("[DenNgay]", "");
            return sms;
        }

        public static string GetTelco(string phone)
        {
            string telco = "";
            string a = "";
            string b = "";
            if (phone.Length == 11 || phone.Length == 12)
            {
                a = phone.Substring(0, 4);
                b = phone.Substring(0, 5);
            }
            if (phone.Length != 11 && phone.Length != 12)
                telco = "";
            // VMS
            else if (a == "8490" || a == "8493" || b == "84120" || b == "84121" || b == "84122" || b == "84126" || b == "84128")
                telco = "VMS";
            // GPC
            else if (a == "8491" || a == "8494" || b == "84123" || b == "84124" || b == "84125" || b == "84127" || b == "84129")
                telco = "GPC";
            // Viettel
            else if (a == "8497" || a == "8498" || b == "84161" || b == "84162" || b == "84163" || b == "84164" || b == "84165" || b == "84166" || b == "84167" || b == "84168" || b == "84169")
                telco = "Viettel";
            // VNM
            else if (a == "8492" || b == "84188" || b == "84186")
                telco = "VNM";
            // SFone
            else if (a == "8495" || b == "84155")
                telco = "Sfone";
            // EVN
            else if (a == "8496")
                telco = "EVN";
            // Gtel
            else if (a == "8499" || b == "84199")
                telco = "Gtel";
            else
                telco = "";
            return telco;
        }

        public static int CountChar(string mes)
        {
            int meslen = 0;
            char[] charExten = { '\\', '\f', '^', '{', '}', '[', ']', '~', '|', '€' };
            foreach (char ch in mes)
            {
                int c = 1;
                for (int i = 0; i < charExten.Length; i++)
                {
                    if (charExten[i].Equals(ch))
                    {
                        c = 2; break;
                    }
                }
                meslen += c;
            }
            return meslen;
        }

        public static byte CountMess(string mes, bool isUnicode)
        {
            int meslen = CountChar(mes);
            byte countmes = 1;
            if (isUnicode)
            {
                if (meslen <= 70) { countmes = 1; }
                else if (meslen > 70 && meslen <= 134) { countmes = 2; }
                else if (meslen > 134 && meslen <= 201) { countmes = 3; }
                else countmes = 4;
            }
            else
            {
                if (meslen <= 160) { countmes = 1; }
                else if (meslen > 160 && meslen <= 306) { countmes = 2; }
                else if (meslen > 306 && meslen <= 459) { countmes = 3; }
                else countmes = 4;
            }
            return countmes;
        }

        public static byte CountRealMess(string mes)
        {
            mes = StringUtil.RemoveSign4VietnameseString(mes);
            int meslen = CountChar(mes);
            byte countmes;

            if (meslen <= 160) { countmes = 1; }
            else if (meslen > 160 && meslen <= 306) { countmes = 2; }
            else if (meslen > 306 && meslen <= 459) { countmes = 3; }
            else countmes = 4;

            return countmes;
        }

        public static void NhanTin()
        {
            if (Class.CompanyInfo.cauhinhdotbaoduong != null && Class.CompanyInfo.cauhinhdotbaoduong != "")
            {
                string[] arrthangnhan = Class.CompanyInfo.cauhinhdotbaoduong.Split(',');
                for (int i = 0; i <= arrthangnhan.Length; i++)
                {
                }
            }
        }
    }
}