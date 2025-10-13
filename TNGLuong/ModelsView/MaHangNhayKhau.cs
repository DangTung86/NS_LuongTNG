using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class MaHangNhayKhau
    {
        public long STT { get; set; }
        public string MaHang { get; set; }
        public byte NhomSize { get; set; }
        public byte ID_CachMay { get; set; }
        public string TenMaHang { get; set; }
        public int SoLuong_CapBTP { get; set; }
    }
    public class MaHangPhanCongDoan
    {
        public long STT { get; set; }
        public string MaHangID { get; set; }
        public string MaHang { get; set; }
        public byte NhomSize { get; set; }
        public byte ID_CachMay { get; set; }
        public string TenMaHang { get; set; }
        public byte ID_NhomCongViec { get; set; }
    }
}