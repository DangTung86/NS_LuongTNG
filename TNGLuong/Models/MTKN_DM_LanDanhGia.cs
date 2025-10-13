using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.Models
{
    public class MTKN_DM_LanDanhGia
    {
        public int ID_LanDanhGia { get; set; }
        public DateTime NgayDanhGia { get; set; }
        public string GhiChu { get; set; }
        public string TenDangNhap_PD { get; set; }
        public DateTime? Ngay_PD { get; set; }
        public string TenDangNhap_Lap { get; set; }
        public DateTime? Ngay_Lap { get; set; }
    }
}