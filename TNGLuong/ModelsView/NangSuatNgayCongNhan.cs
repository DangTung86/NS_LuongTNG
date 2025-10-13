using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNGLuong.ModelsView
{
    public class NangSuatNgayCongNhan
    {
        public int MaHang_ID { get; set; }
        public string MaHang { get; set; }
        public string TenCongDoan { get; set; }
        public decimal ThoiGian { get; set; }
        public int KeHoach_NhanVien { get; set; }
        public decimal DonGia { get; set; }        
        public int ThucHien_NhanVien { get; set; }
        public decimal ThanhTien { get; set; }
        public decimal HieuSuat { get; set; }
        public decimal TienLuong { get; set; }
        public decimal HS_MaHang { get; set; }
        public string TenPhongban { get; set; }
    }
}