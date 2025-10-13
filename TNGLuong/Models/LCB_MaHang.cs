namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_MaHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(250)]
        public string MaHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhongBanID { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte NhomSize { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte ID_CachMay { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Thang { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Nam { get; set; }

        [Required]
        [StringLength(2)]
        public string MaChiNhanh { get; set; }

        public int DonViID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBatDauTinhNangSuat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayVaoChuyen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        public decimal? NangSuatBQ { get; set; }

        public decimal? HS_NgayCuoi { get; set; }

        public int SoNgay_SanXuat { get; set; }

        public decimal HS_BinhQuan { get; set; }

        public decimal TongLuongCB { get; set; }

        public decimal? DonGiaTKC { get; set; }

        public decimal? TGCN { get; set; }

        public decimal K1 { get; set; }

        public int TongSanPham { get; set; }

        [StringLength(50)]
        public string TenDangNhap_PheDuyet { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Ngay_PheDuyet { get; set; }

        public byte TrangThai { get; set; }
    }
}
