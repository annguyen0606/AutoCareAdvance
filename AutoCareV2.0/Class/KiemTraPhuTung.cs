using System.Collections.Generic;
using System.Linq;

namespace AutoCareV2._0.Class
{
    public class KiemTraPhuTung
    {
        #region Properties

        public static List<PhuTung> ListPhuTung;

        #endregion Properties

        #region Function

        //Lấy tất cả các phụ tùng có số lượng nhỏ hơn ngưỡng
        public static List<PhuTung> KiemTraSoLuongPhuTung(List<PhuTung> _ListPhuTung, int _IdCongTy)
        {
            List<PhuTung> ListResult = new List<PhuTung>();

            if (_ListPhuTung != null && _ListPhuTung.Count > 0)
            {
                ListResult = _ListPhuTung.AsParallel().Where(m => m.IdCongTy == _IdCongTy
                    && m.SoLuong <= m.NguongSoLuong).ToList();
            }

            return ListResult;
        }

        #endregion Function
    }
}