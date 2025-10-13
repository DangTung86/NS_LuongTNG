using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class ListNangSuatToTruongPhan
    {
        public string MaHang { get; set; }
        public int PhongBanID { get; set; }
        public byte NhomSize { get; set; }
        public byte ID_CachMay { get; set; }
        public int ID_CongDoan { get; set; }
        public int MaNS_ID { get; set; }
        public DateTime Ngay { get; set; }
        public int PhongBanID_NS { get; set; }
        public int KeHoach_NhanVien { get; set; }
        public int ThucHien_NhanVien { get; set; }
        public string TenPhongBan { get; set; }        
        public string TenCongDoan { get; set; }         
    }
}