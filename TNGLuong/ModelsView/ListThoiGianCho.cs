using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class ListThoiGianCho
    {
        public int STT { get; set; }
        public string Ten_LyDoNgungViec { get; set; }
        public byte ID_LyDoNgungViec { get; set; }
        public DateTime? ThoiGian_BatDau { get; set; }
        public DateTime? ThoiGian_KetThuc { get; set; }
        public decimal ThoiGian { get; set; }
        public DateTime? ThoiGian_XacNhan_BatDau { get; set; }
        public DateTime? ThoiGian_XacNhan_KetThuc { get; set; }
        public string GhiChu { get; set; }
        public int MaNS_ID { get; set; }
        public bool XacNhan { get; set; }
        public bool PheDuyet { get; set; }
        public int ID_ThoiGianNgungViec { get; set; }
    }
}