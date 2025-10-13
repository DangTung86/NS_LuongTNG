using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.Cls_DangKyAnCa
{
    public class clsDangKy_AnCa
    {
        public int? MaNS_ID { get; set; }
        public DateTime? Ngay { get; set; }
        public bool? AnCa { get; set; }
        public string ThuTV { get; set; }
        public byte? weekday_id { get; set; }
        public string GhiChu { get; set; }
    }
}