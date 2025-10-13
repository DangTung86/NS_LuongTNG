using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class InfoUserName
    {
        public int? MaNS_ID { get; set; }
        public string MaNS { get; set; }
        public string HoTen { get; set; }
        public string PassWord { get; set; }
        public int? PhongbanID { get; set; }
        public int? ToMay { get; set; }
        public int? ToTruong { get; set; }
        public int? DonViID { get; set; }
        public string TenDonVi { get; set; }
        public string TenPhongban { get; set; }
        public int? DoiTuongID { get; set; }
        public string AvatarUrl { get; set; }
        public byte? ID_NhomCongViec { get; set; }
        public int IsHetHanHD { get; set; }
    }
    public class ListCongDiLam
    {
        public DateTime? CS_GioVao { get; set; }
        public DateTime? CS_GioRa { get; set; }
        public string Ngay { get; set; }
        public int MaNS_ID { get; set; }
        public string TenCa { get; set; }
    }
}