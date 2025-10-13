using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class ListNhanSuTheoPhongBan
    {
        public int ID { get; set; }
        public string MaNS { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string TenDonVi { get; set; }
        public string TenPhongBan { get; set; }
        public int DonViID { get; set; }
        public int PhongBanID { get; set; }
        public string TrangThaiNS { get; set; }
        public string SoCMT { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
    }
}