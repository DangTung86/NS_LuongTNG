using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.Models
{
    public class MTKN_NhanSu_LoaiMay_XepHang
    {            
        public int ID_LoaiMay { get; set; }
        public int MucTieu_LoaiMay { get; set; }
        public string TenLoaiMay { get; set; }
        public int ThucHien_LoaiMay { get; set; }
        public int DaoTaoThem { get; set; }
    }
}   