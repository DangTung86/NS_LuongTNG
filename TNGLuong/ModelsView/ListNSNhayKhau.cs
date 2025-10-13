using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class ListNSNhayKhau
    {
        public ListNSNhayKhau()
        {

        }
        public string MaHang { get; set; }
        public string TenCongDoan { get; set; }  
        public decimal DonGia { get; set; }
        public string PheDuyet { get; set; }
        public decimal TGCN { get; set; }
        public string BacCapDo { get; set; }
        public string BacKyNang { get; set; }
        public int CongNhan { get; set; }
        public int PhongBanID { get; set; }
        public byte NhomSize { get; set; }
        public byte ID_CachMay { get; set; }
        public int ID_CongDoan { get; set; }
        public int MaNS_ID { get; set; }
        public DateTime Ngay { get; set; }
        public int LuyKe { get; set; }
        public int SoLuong_CapBTP { get; set; }
        public int IsBTP { get; set; }
        public int IsKhoaMaHang { get; set; }

        public ListNSNhayKhau(string mahang,string tencongdoan,string pheduyet,decimal dongia,string baccapdo, int congnhan,int phongbanid, byte nhomsize,byte idcachmay,int idcongdoan, int mansid, DateTime ngay, int _LuyKe, int SolUongBTP, int _IsBTP)
        {
            this.MaHang = mahang;
            this.TenCongDoan = tencongdoan;
            this.PheDuyet = pheduyet;
            this.DonGia = dongia;
            this.BacCapDo = baccapdo;
            this.CongNhan = congnhan;
            this.PhongBanID = phongbanid;
            this.NhomSize = nhomsize;
            this.ID_CachMay = idcachmay;
            this.ID_CongDoan = idcongdoan;
            this.MaNS_ID = mansid;
            this.Ngay = ngay;
            this.LuyKe = _LuyKe;
            this.SoLuong_CapBTP = SolUongBTP;
            this.IsBTP = IsBTP;
        }
    }
}