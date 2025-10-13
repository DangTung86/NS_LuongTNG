using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class ListCongDoanCN
    {
        public int ID_Cum { get; set; }
        public string TenCum { get; set; }
        public int ID_CongDoan { get; set; }
        public string TenCongDoan { get; set; }
        public byte BacTho { get; set; }
        public decimal TGCN { get; set; }
        public decimal DonGia { get; set; }
        public decimal HeSoK { get; set; }
        public int STT_Cum { get; set; }
        public int STT_CongDoan { get; set; }
        public byte TonTai { get; set; }
    }
    public class ListKeHoachCapNhatTuDong
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaHang { get; set; }
        public int PhongBanID { get; set; }
        public byte NhomSize { get; set; }
        public byte ID_CachMay { get; set; }
        public DateTime Ngay_ApDung { get; set; }
      
    }
}