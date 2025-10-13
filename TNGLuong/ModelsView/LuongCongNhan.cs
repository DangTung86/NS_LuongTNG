using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class LuongCongNhan
    {
        public int PhongBanID { get; set; }
        public int MaNS_ID { get; set; }
        public decimal GioLamViec { get; set; }
        public decimal GioChoViec { get; set; }
        public decimal LuongCB { get; set; }
        public decimal NangSuat { get; set; }
        public decimal NhayKhau { get; set; }
        public decimal LuongSP { get; set; }
        public decimal VuotNangSuat { get; set; }
        public decimal LuongThemGio { get; set; }
        public decimal LuongChoViec { get; set; }
        public decimal TongTienLuong { get; set; }
        public bool PheDuyet { get; set; }
    }
}