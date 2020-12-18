using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AutoCareUtil
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
            return str;
        }
    }
    public class Utilities
    {
        #region Random String
        public static string RandomString(int size)
        {
            Random rnd = new Random();
            string srds = "";
            string[] str = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < size; i++)
            {
                srds = srds + str[rnd.Next(0, 61)];
            }
            return srds;
        }
        #endregion

        #region Encode
        public static string Encode(string str)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            string strtemp = Convert.ToBase64String(encbuff);
            string strtam = "";
            Int32 i = 0, len = strtemp.Length;
            for (i = 3; i <= len; i += 3)
            {
                strtam = strtam + strtemp.Substring(i - 3, 3) + RandomString(1);
            }
            strtam = strtam + strtemp.Substring(i - 3, len - (i - 3));
            return strtam;
        }
        #endregion
        /// <summary>
        /// Giai ma mot chuoi
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        #region Decode
        public static string Decode(string str)
        {
            string strtam = "";
            Int32 i = 0, len = str.Length;
            for (i = 4; i <= len; i += 4)
            {
                strtam = strtam + str.Substring(i - 4, 3);
            }
            strtam = strtam + str.Substring(i - 4, len - (i - 4));
            byte[] decbuff = Convert.FromBase64String(strtam);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        #endregion

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

        public static string smsreplace(string sms, string tenkh, string ngaysinh, string thuonghieu, string tenxe,
            string bienso, string sokhung, string somay, string sodienthoai, string solanbd, string ngayBaoDuong,
            string tuNgay, string denNgay)
        {
            sms = sms.Replace("[TenKH]", tenkh);
            sms = sms.Replace("[NgaySinh]", ngaysinh);
            sms = sms.Replace("[ThuongHieu]", thuonghieu);
            sms = sms.Replace("[Tenxe]", tenxe);
            sms = sms.Replace("[Bienso]", bienso);
            sms = sms.Replace("[Sokhung]", sokhung);
            sms = sms.Replace("[Somay]", somay);
            sms = sms.Replace("[SoDienThoai]", sodienthoai);
            sms = sms.Replace("[SolanBD]", solanbd);
            sms = sms.Replace("[NgayBaoDuong]", ngayBaoDuong);
            sms = sms.Replace("[TuNgay]", tuNgay);
            sms = sms.Replace("[DenNgay]", denNgay);
            return sms;
        }

        public static string GetTelco(string phone)
        {
            string Telco = "";
            string a = "";
            string b = "";

            if ((phone.Length != 12 && phone.StartsWith("841")) || (phone.Length != 11 && phone.StartsWith("849")))
            {
                return Telco;
            }

            if (phone.Length == 11 || phone.Length == 12)
            {
                a = phone.Substring(0, 4);
                b = phone.Substring(0, 5);
            }
            if (phone.Length != 11 && phone.Length != 12)
                Telco = "";
            // VMS
            else if (a == "8490" || a == "8493" || a == "8489" || b == "84120" || b == "84121" || b == "84122" || b == "84126" || b == "84128")
                Telco = "VMS";
            // GPC 
            else if (a == "8491" || a == "8494" || a == "8488" || b == "84123" || b == "84124" || b == "84125" || b == "84127" || b == "84129")
                Telco = "GPC";
            // Viettel 
            else if (a == "8496" || a == "8497" || a == "8498" || a == "8486" || b == "84162" || b == "84163" || b == "84164" || b == "84165" || b == "84166" || b == "84167" || b == "84168" || b == "84169")
                Telco = "Viettel";
            // VNM
            else if (a == "8492" || a == "8456" || b == "84188" || b == "84186")
                Telco = "VNM";
            // SFone
            else if (a == "8495" || b == "84155")
                Telco = "Sfone";
            // EVN
            else if (a == "8496")
                Telco = "Viettel";
            // Gtel
            else if (a == "8499" || b == "84199")
                Telco = "Gtel";
            else if (a == "8452" || a == "8456")
                Telco = "VNM";
            else if (a == "8484" || a == "8481" || a == "8482" || a == "8483" || a == "8485")
                Telco = "GPC";
            else if (a == "8470" || a == "8479" || a == "8477" || a == "8476" || a == "8478")
                Telco = "VMS";
            else if (a == "8439" || a == "8438" || a == "8437" || a == "8436" || a == "8435"
                || a == "8434" || a == "8433" || a == "8432")
                Telco = "Viettel";
            else
                Telco = "";

            return Telco;
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
    }
}
