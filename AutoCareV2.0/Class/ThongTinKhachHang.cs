using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCareV2._0.Class
{
    public class ThongTinKhachHang
    {

        public long id { get; set; }
        public string email { get; set; }
        public string phone_no { get; set; }
        public string username { get; set; }
        public string note { get; set; }
        public string address { get; set; }
        public int gender { get; set; }
        public List<Parameter> custom_fields { get; set; }


    }
    public class contactdata
    {
        public string code { get; set; }
        public ThongTinKhachHang contact { get; set; }
    }
    public class danhsachKH
    {
        public int id { get; set; }
        public string username { get; set; }
        public string phone_no { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public string gender { get; set; }
    }
    public class multicontactdata
    {
        public string code { get; set; }
        public int numFound { get; set; }
        public List<danhsachKH> contact { get; set; }
    }
    public class ErrorRequest
    {
        public string code { get; set; }
        public string message { get; set; }
        public extradata extra_data { get; set; }
    }
    public class extradata
    {
        public string duplicate_id { get; set; }
    }
}
