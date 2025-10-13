using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class ListCongDoan_KeHoach
    {
        public int ID_CongDoan { get; set; }
        public int MaNS_ID { get; set; }
        public decimal TyLe_Giao { get; set; }
        public DateTime Ngay_ApDung { get; set; } 
        public decimal TongTyLe { get; set; }
    }
    public class ListCongDoan_KeHoach_TenCongDoan
    {
        public int ID_CongDoan { get; set; }
        public int MaNS_ID { get; set; }
        public float TyLe_Giao { get; set; }
        public string NgayApDung { get; set; }
        public string TenCongDoan { get; set; }
        public string TenCum { get; set; }
        public float TongTyLe { get; set; }
    }

    public class ListCongDoan_KeHoach_CongNhan
    {
        public int ID_CongDoan { get; set; }   
        public int MaNS_ID { get; set; }
        public string TyLe_Giao { get; set; }
        public string NgayApDung { get; set; }
        public string TenCongDoan { get; set; }
        public string TenCum { get; set; }
        public string TongKH { get; set; }
        public decimal TGCN { get; set; }
        public string HeSoK { get; set; }
        public decimal DonGia { get; set; }
    }
    public class ListCongDoan_KeHoach_NhanSu
    {
        public int ID_CongDoan { get; set; }
        public int MaNS_ID { get; set; }
        public string TyLe_Giao { get; set; }
        public DateTime Ngay_ApDung { get; set; }
        public string HoTen { get; set; }

    }
}