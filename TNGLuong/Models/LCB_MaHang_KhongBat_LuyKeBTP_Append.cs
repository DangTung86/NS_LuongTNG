using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.Models
{
    public class LCB_MaHang_KhongBat_LuyKeBTP_Append
    {
        public int Id_BTP { get; set; }
        public string MaHang { get; set; }
        public int PhongBanID { get; set; }
        public string TenPhongban { get; set; }
        public string TenDonVi { get; set; }
    }
}