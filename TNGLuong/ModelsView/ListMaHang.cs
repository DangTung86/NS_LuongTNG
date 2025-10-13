using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class ListMaHang
    {
        public string MaHang { get; set; }
        public int MaHang_ID { get; set; }
        public bool IsKhoaMaHang { get; set; }
        public string SoLuong_CapBTP { get; set; }
        public string HieuSuat { get; set; }
        public bool IsKCS { get; set; }
        public bool IsCNPT { get; set; }
    }
}