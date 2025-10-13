using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class ListNangSuat
    {
        public string MaHang { get; set; }
        public int MaHangID { get; set; }
        public int PhongBanID { get; set; }
        public int NhomSize { get; set; }
        public int ID_CachMay { get; set; }
        public int ID_CongDoan { get; set; }
        public int MaNS_ID { get; set; }
        public DateTime Ngay { get; set; }
        public int PhongBanID_NS { get; set; }
        public string BacCapDo { get; set; }
        public string BacKyNang { get; set; }
        public int KeHoach_NhanVien { get; set; }
        public int ThucHien_NhanVien { get; set; }
        public string DaPD { get; set; }        
        public int LuyKe { get; set; }
        public int LuyKe_CongDoan { get; set; }
        public decimal HieuSuat { get; set; }
        public string TenMaHang { get; set; }
        public string TenCongDoan { get; set; }
        public decimal TGCN { get; set; }
        public decimal DonGia { get; set; }        
        public int STT_Cum { get; set; }
        public int ID_Cum { get; set; }
        public int STT_CongDoan { get; set; }
        public byte TonTai { get; set; }
        public bool PheDuyet { get; set; }
        public string TenPhongban { get; set; }
        public double Tyle { get; set; }       
        public int SoLuong_CapBTP { get; set; }
        public bool IsKhoaMaHang { get; set; }
        public bool IsBTP { get; set; }
    }
}