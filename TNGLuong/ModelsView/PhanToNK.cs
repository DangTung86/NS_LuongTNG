using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class PhanToNK
    {
        public int MaNS_ID { get; set; }
        public string MaNS { get; set; }
        public int PhongBanID_NS { get; set; }
        public string HoTen { get; set; }
        public int? ToNK1 { get; set; }
        public int? ToNK2 { get; set; }
        public int? ToNK3 { get; set; }
    }
}