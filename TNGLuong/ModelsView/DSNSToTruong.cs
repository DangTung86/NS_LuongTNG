using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class DSNSToTruong
    {
        public string MaHang { get; set; }
        public int PhongBanID { get; set; }
        public byte NhomSize { get; set; }
        public byte ID_CachMay { get; set; }
        public int ID_CongDoan { get; set; }
        public int MaNS_ID { get; set; }
        public DateTime Ngay { get; set; }
        public string TenCongDoan { get; set; }
        public decimal DonGia { get; set; }
        public int CongNhan { get; set; }
        public int LuyKe { get; set; }
        public int TT { get; set; }
        public long STT_CongDoan { get; set; }
    }
}