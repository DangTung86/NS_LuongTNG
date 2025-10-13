using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class ListCongDiLamCongNhan
    {

        public DateTime? CS_GioVao { get; set; }
        public DateTime? CS_GioRa { get; set; }
        public DateTime? Ngay { get; set; }
        public int MaNS_ID { get; set; }
        public string HoTen { get; set; }
        public string TenCa { get; set; }
        public string MaNS { get; set; }
        public int STT { get; set; }
    }
}