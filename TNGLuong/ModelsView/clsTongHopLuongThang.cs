using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class clsTongHopLuongThang
    {
        public long STT { get; set; }
        public int MaNS_ID { get; set; }
        public string HoTen { get; set; }
        public string MaNS { get; set; }
        public decimal TLThang { get; set; }
        public decimal BLThang { get; set; }
        public decimal TLNgay { get; set; }
        public decimal BLNgay { get; set; }
        public decimal TTLThang { get; set; }
        public int SLBLThang { get; set; }
        public decimal TTLNgay { get; set; }
        public int SLBLNgay { get; set; }
        public string PheDuyet { get; set; }
    }
}