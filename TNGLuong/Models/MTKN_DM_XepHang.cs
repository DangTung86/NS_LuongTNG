using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.Models
{
    public class MTKN_DM_XepHang
    {
        public int ID_XepHang { get; set; }
        public int Diem { get; set; }
        public string XepHang { get; set; }
        public string GhiChu { get; set; }
        public string TenDangNhap_Lap { get; set; }
        public DateTime? Ngay_Lap { get; set; }
    }
}