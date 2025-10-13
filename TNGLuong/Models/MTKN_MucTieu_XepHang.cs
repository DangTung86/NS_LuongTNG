using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.Models
{
    public class MTKN_MucTieu_XepHang
    {
        public int ID_MucTieu { get; set; }
        public int PhongBanID { get; set; }
        public int? DonViID { get; set; }
        public int ID_LoaiMay { get; set; }
        public string TenLoaiMay { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public int? TyLeDat { get; set; }
        public int? ThucHien { get; set; }
        public int TrangThai { get; set; }
        public string TenTrangThai { get; set; }
        public int TonTai { get; set; }
        public int SoNguoi { get; set; }
        public int SoNS { get; set; }
        public int? ConLai { get; set; }
    }
}