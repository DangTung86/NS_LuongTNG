using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class clsTongHopLuong_ChiTietNangSuat
    {
        public string Style { get; set; }
        public string TenPhongban { get; set; }
        public string TenCongDoan { get; set; }
        public decimal TGCN { get; set; }
        public int LKKeHoach { get; set; }
        public int LKThucHien { get; set; }
        public int KeHoach { get; set; }
        public int ThucHien { get; set; }
    }
}