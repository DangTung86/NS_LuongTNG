using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class clsPhanCumTruong
    {
        public long STT { get; set; }
        public int ID_Cum { get; set; }
        public string TenCum { get; set; }
        public int MaNS_ID { get; set; }
        public int PhongBanID { get; set; }
    }
}