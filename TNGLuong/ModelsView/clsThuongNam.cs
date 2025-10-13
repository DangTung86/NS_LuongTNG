using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class clsThuongNam
    {
        public int ThuongNamID { get; set; }
        public int MaNS_ID { get; set; }
        public string MaNS { get; set; }
        public string HoTen { get; set; }
        public string SoCMT { get; set; }
        public string DienThoai { get; set; }
        public string TheATM { get; set; }
        public decimal Tong_LuongSP { get; set; }
        public decimal Tong_ThuongABC { get; set; }
        public int SoThang_LamViec { get; set; }
        public int SoThang_XetThuong { get; set; }
        public int SoThang_BuLuong { get; set; }
        public int SoThang_KyLuat { get; set; }
        public int SoThang_KyLuat_BuLuong { get; set; }
        public int? SoThang_LoaiA { get; set; }
        public bool? isLaoDong_TienTien { get; set; }
        public bool? isChienSy_ThiDua { get; set; }
        public bool? isThanhTich_Cao { get; set; }
        public decimal Luong_Thang13 { get; set; }
        public decimal Thuong_ABC { get; set; }
        public decimal Thuong_ThangLamViec { get; set; }
        public decimal Thuong_TienTien { get; set; }
        public decimal Thuong_CSTD { get; set; }
        public decimal Thuong_TTC { get; set; }
        public decimal? TienChia { get; set; }
        public decimal Thuong_ToiThieu { get; set; }
        public decimal Thuong_Khac { get; set; }
        public decimal Tong_ThuongNam { get; set; }
        public decimal? ThueTNCN { get; set; }
        public decimal ThucNhan { get; set; }
        public decimal HeSoK1 { get; set; }
        public decimal HeSoK2 { get; set; }
        public decimal HeSoK3 { get; set; }
        public decimal HeSoK4 { get; set; }
        public decimal HeSoK5 { get; set; }
        public decimal HeSoK6 { get; set; }
        public int SoThang_Muc1 { get; set; }
        public int SoThang_Muc2 { get; set; }
        public int SoThang_Muc3 { get; set; }
        public int SoThang_Muc4 { get; set; }
        public decimal Muc1 { get; set; }
        public decimal Muc2 { get; set; }
        public decimal Muc3 { get; set; }
        public decimal Muc4 { get; set; }
        public decimal Muc5 { get; set; }
        public decimal Tong_ThuNhap { get; set; }
        public decimal ThuNhapBQ { get; set; }
        public decimal TyLe_Thuong_TTN { get; set; }
        public decimal TyLe_Thuong_TNBQ { get; set; }
        public decimal TienLuong_BinhQuan { get; set; }
        public decimal TyLeThuong_KQSXKD { get; set; }
        public decimal Tong_Cong { get; set; }
        public decimal Thuong_ToiDa { get; set; }
        public decimal HeSoK { get; set; }
        public int TrangThai { get; set; }
    }
}