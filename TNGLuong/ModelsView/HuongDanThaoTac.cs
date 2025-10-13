using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class HuongDanThaoTac
    {
        public Int16 ID_NhomThaoTac { get; set; }
        public string Ten_NhomThaoTac { get; set; }
        public Int16 ID_ThaoTac { get; set; }
        public string Ten_ThaoTac { get; set; }
        public string Ma_ThaoTac { get; set; }
        public string CuDong { get; set; }
        public decimal TMU_ThaoTac { get; set; }
        public byte? ID_KhoangCachBTP { get; set; }
        public decimal TMU { get; set; }
        public decimal TanSuat { get; set; }
        public decimal HaoPhi { get; set; }
        public decimal? TG_ThaoTac { get; set; }
        public bool Chon { get; set; }
    }
}