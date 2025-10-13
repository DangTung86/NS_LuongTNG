using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace TNGLuong
{
    public class clsBangLuongThang
    {
        public int BangLuongID { get; set; }
        public int MaNS_ID { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string TenDonVi { get; set; }
        public string TenBoPhan { get; set; }
        public string MaNS { get; set; }
        public string HoTen { get; set; }
        public string SoCMT { get; set; }
        public string SoTaiKhoan { get; set; }
        public string SoDienThoai { get; set; }
        public decimal SoCong { get; set; }
        public decimal SoGio_LamThem { get; set; }
        public decimal LuongCB { get; set; }
        public decimal Luong_DongBaoHiem { get; set; }
        public decimal MucLuongCB { get; set; }
		public decimal PC_ChucVu { get; set; }
        public decimal PC_PCCC { get; set; }
		public decimal PC_ATVSV { get; set; }
		public decimal TL_CapBac { get; set; }
		public decimal PC_Luong { get; set; }
		public decimal PC_PhuNu { get; set; }
		public decimal TL_VNS_HQCV { get; set; }
		public decimal TL_ThoiGian { get; set; }
		public decimal TL_ThemGioNgayThuong { get; set; }
		public decimal TL_ThemGioNgayNghi { get; set; }
		public decimal TL_KiemNhiem { get; set; }
		public decimal Thuong_XepHang { get; set; }
		public decimal PC_ConNho { get; set; }
		public decimal PC_AnCa { get; set; }
		public decimal PC_XangXe { get; set; }
        public decimal PC_Khac { get; set; }
        public decimal PC_HoTroTayNgheYeu { get; set; }
        public decimal PC_ChuyenCan { get; set; }
        public decimal TongThuNhap { get; set; }
		public decimal KT_BaoHiem { get; set; }
		public decimal KT_ThueTNCN { get; set; }
		public decimal KT_DangPhi { get; set; }
		public decimal KT_CongDoan { get; set; }
		public decimal KT_DoanPhi { get; set; }
		public decimal KT_Khac { get; set; }
		public decimal SoTien_ConNhan { get; set; }
		public decimal TN_ABC { get; set; }
		public decimal TN_LDTT { get; set; }
		public decimal TN_CSTD { get; set; }
		public decimal TN_LuongThang13 { get; set; }
		public decimal TL_Luong_ThemGio { get; set; }
		public decimal TL_LuongSP_8h { get; set; }
		public decimal T_Khac { get; set; }
		public decimal SoCong_ThoiGian { get; set; }
        public string MaDonVi { get; set; }
        public int LoaiBangLuong { get; set; }
        public string XepLoai { get; set; }
        public int TrangThai { get; set; }
    }
}