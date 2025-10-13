using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.Models
{
    public class MTKN_MucTieu_LoaiMay
    {
        public int MaNS_ID { get; set; }
        public string HoTen { get; set; }
        public int? ID_XepHang { get; set; }
        public int? ID{ get; set; }
        public string XepHang { get; set; }
        public string XepHangMucTieu { get; set; }
        public int? ID_XepHang_MucTieu { get; set; }

    }
}