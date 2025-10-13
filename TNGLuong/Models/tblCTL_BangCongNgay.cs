namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblCTL_BangCongNgay
    {
        
        public int ID { get; set; }
        
        public int MaNS_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string MaSoThe { get; set; }

        [Required]
        public int DonViID { get; set; }

        [Required]
        public int PhongBanID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Ngay { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? GioVao { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? GioRa { get; set; }

        [StringLength(5)]
        public string Ma_Ca { get; set; }

        public decimal? GioThem { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }

        [StringLength(50)]
        public string TenDangNhapLap { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayLap { get; set; }

        public int TrangThai { get; set; }
    }
}
