using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.Models
{
    public class MTKN_XepHang_NhanSu
    {
        public int ID { get; set; }
        public int ID_NhanSu { get; set; }
        public int ID_LoaiMay { get; set; }
        public string TenLoaiMay { get; set; }
        public int ID_XepHang { get; set; }
        public int ID_LanDanhGia { get; set; }
        public int? ID_XepHang_MucTieu { get; set; }
    }
}