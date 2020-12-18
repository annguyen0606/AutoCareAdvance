using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AutoCareV2._0.Class
{
    public class ChangeOilByKM
    {
        SqlDataProvider sqlPrv = new SqlDataProvider();

        public bool IsUseChangeOilByKM(string IdCongTy)
        {
            try
            {
                return bool.Parse(sqlPrv.ExecuteScalar(@"if EXISTS (select * from SmsChangeOilConfig where IdCongTy=" + IdCongTy + @")
	                BEGIN
		                select IsUse from SmsChangeOilConfig where IdCongTy=" + IdCongTy + @"
	                END
                ELSE
	                BEGIN
		                select 'False'
	                END").ToString());
            }
            catch
            {
                return false;
            }
        }

        #region SendSmsChangeOilNormal
        public void SendSmsChangeOilNormal(string IDKhachHang,string IdBaoDuong,int KmCurrent,string SoKhung,string IdCongTy)
        {
            int KMCount = 0;
            int KmDifferent = 0;
            #region Get data condition
            string DateSendAfter = "0";
            string SmsContent = "";
            int KmToReach = 0;
            TimeSpan TimeSend = new TimeSpan();
            try
            {
                KMCount = int.Parse(sqlPrv.ExecuteScalar("select top 1 SoKm from LichSuBaoDuongXe where (IdKhachHang="+IDKhachHang+" or SoKhung='"+SoKhung+"') and ThayDau=1 and IdBaoDuong!="+IdBaoDuong+" order by NgayGiaoXe desc").ToString());
                KmDifferent = KmCurrent - KMCount;
                DataTable SmsChangeOilConfig = sqlPrv.GetData("select * from SmsChangeOilConfig where IdCongTy=" + IdCongTy + " and IsUse=1");

                if (SmsChangeOilConfig.Rows.Count>0)
                {
                    DateSendAfter = SmsChangeOilConfig.Rows[0]["DateSendAfter"].ToString();
                    SmsContent = SmsChangeOilConfig.Rows[0]["SmsContent"].ToString();
                    KmToReach = int.Parse(SmsChangeOilConfig.Rows[0]["KmToReach"].ToString());
                    TimeSend = TimeSpan.Parse(SmsChangeOilConfig.Rows[0]["TimeSend"].ToString());
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {return;}
            #endregion

            if (KmToReach<=KmDifferent)
            {
                string CustName = "Quy khach";
                string TenCongTy= sqlPrv.ExecuteScalar("select top 1 TenCongTy from CongTy where IdCongTy=" + IdCongTy).ToString();
                string Phone="";
                string SenderName=Class.CompanyInfo.sendername;
                if (IDKhachHang!="")
                {
                    DataTable dtCustInfo = sqlPrv.GetData("select * from KhachHang where IdKhachHang=" + IDKhachHang);
                    if (dtCustInfo.Rows.Count>0)
                    {
                        CustName = dtCustInfo.Rows[0]["TenKH"].ToString();
                        Phone=dtCustInfo.Rows[0]["DienThoai"].ToString();
                    }
                    SmsContent = SmsContent.Replace("[SoKM]", KmCurrent.ToString()).Replace("[HoTen]", CustName).Replace("[TenCongTy]", TenCongTy);
                    
                    bool isUnicode=Tools.GetDataCoding(SmsContent) == 8 ? true : false;
                    byte CountMess=Utilities.CountMess(SmsContent, isUnicode);

                    string DateTimeSend = DateTime.Now.AddDays(Convert.ToDouble(int.Parse(DateSendAfter))).ToString("yyyy-MM-dd ") + TimeSend.ToString();

                    SqlCommand cmd=new SqlCommand();
                    cmd.CommandText = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule) values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@sendername", Class.CompanyInfo.sendername);
                    cmd.Parameters.AddWithValue("@phone", Phone);
                    cmd.Parameters.AddWithValue("@sms", SmsContent);
                    cmd.Parameters.AddWithValue("@countmes", CountMess);
                    cmd.Parameters.AddWithValue("@smstype", "Thay dau theo km");
                    cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@idkhachhang", IDKhachHang);
                    cmd.Parameters.AddWithValue("@timeSchedule", DateTimeSend);
                    sqlPrv.ExecuteNonQuery(cmd);
                }
            }
        }
        #endregion

        #region SendSmsChangeOilDauMay
        public void SendSmsChangeOilDauMay(string IDKhachHang, string IdBaoDuong, int KmCurrent, string SoKhung, string IdCongTy)
        {
            int KMCount = 0;
            int KmDifferent = 0;
            #region Get data condition
            string DateSendAfter = "0";
            string SmsContent = "";
            int KmToReach = 0;
            TimeSpan TimeSend = new TimeSpan();
            try
            {
                KMCount = int.Parse(sqlPrv.ExecuteScalar("select top 1 SoKm from LichSuBaoDuongXe where (IdKhachHang=" + IDKhachHang + " or SoKhung='" + SoKhung + "') and ThayDauMay=1 and IdBaoDuong!=" + IdBaoDuong + " order by NgayGiaoXe desc").ToString());
                KmDifferent = KmCurrent - KMCount;
                DataTable SmsChangeOilConfig = sqlPrv.GetData("select * from SmsChangeOilConfig where IdCongTy=" + IdCongTy + " IsUse=1");

                if (SmsChangeOilConfig.Rows.Count > 0)
                {
                    DateSendAfter = SmsChangeOilConfig.Rows[0]["DateSendAfter"].ToString();
                    SmsContent = SmsChangeOilConfig.Rows[0]["SmsContent"].ToString();
                    KmToReach = int.Parse(SmsChangeOilConfig.Rows[0]["KmToReach"].ToString());
                    TimeSend = TimeSpan.Parse(SmsChangeOilConfig.Rows[0]["TimeSend"].ToString());
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            { return; }
            #endregion

            if (KmToReach <= KmDifferent)
            {
                string CustName = "Quy khach";
                string TenCongTy = sqlPrv.ExecuteScalar("select top 1 TenCongTy from CongTy where IdCongTy=" + IdCongTy).ToString();
                string Phone = "";
                string SenderName = Class.CompanyInfo.sendername;
                if (IDKhachHang != "")
                {
                    DataTable dtCustInfo = sqlPrv.GetData("select * from KhachHang where IdKhachHang=" + IDKhachHang);
                    if (dtCustInfo.Rows.Count > 0)
                    {
                        CustName = dtCustInfo.Rows[0]["TenKH"].ToString();
                        Phone = dtCustInfo.Rows[0]["DienThoai"].ToString();
                    }
                    SmsContent = SmsContent.Replace("[SoKM]", KmCurrent.ToString()).Replace("[HoTen]", CustName).Replace("[TenCongTy]", TenCongTy);

                    bool isUnicode = Tools.GetDataCoding(SmsContent) == 8 ? true : false;
                    byte CountMess = Utilities.CountMess(SmsContent, isUnicode);

                    string DateTimeSend = DateTime.Now.AddDays(Convert.ToDouble(int.Parse(DateSendAfter))).ToString("yyyy-MM-dd ") + TimeSend.ToString();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule) values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@sendername", Class.CompanyInfo.sendername);
                    cmd.Parameters.AddWithValue("@phone", Phone);
                    cmd.Parameters.AddWithValue("@sms", SmsContent);
                    cmd.Parameters.AddWithValue("@countmes", CountMess);
                    cmd.Parameters.AddWithValue("@smstype", "Thay dau theo km");
                    cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@idkhachhang", IDKhachHang);
                    cmd.Parameters.AddWithValue("@timeSchedule", DateTimeSend);
                    sqlPrv.ExecuteNonQuery(cmd);
                }
            }
        }
        #endregion
    }
}
