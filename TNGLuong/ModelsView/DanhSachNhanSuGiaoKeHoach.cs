using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class DanhSachNhanSuGiaoKeHoach
    {
        public int MaNS_ID { get; set; }
        public string MaNS { get; set; }
        public string HoTen { get; set; }
        public byte BacLuong { get; set; }
        public byte? Loai { get; set; }
        public int? PhongBanID_NS { get; set; }
        public decimal KeHoach_TienLuong { get; set; }
        public int? NhomNguoiDungID { get; set; }
        public decimal? HeSoLuong { get; set; }
        public byte? ID_CheDo { get; set; }
        public string KyHieu { get; set; }
        public decimal? TGLV { get; set; }
        public Int16? ID_NhomNhanVien { get; set; }
        public string Ten_NhomNhanVien { get; set; }
        public byte? STT_NhomNhanVien { get; set; }
        public bool? DaThanhLy { get; set; }
       
    }
}