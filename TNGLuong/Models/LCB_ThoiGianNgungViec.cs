namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_ThoiGianNgungViec
    {
        [Key]
        public int ID_ThoiGianNgungViec { get; set; }

        public int MaNS_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Ngay { get; set; }

        public int PhongBanID { get; set; }

        public byte ID_LyDoNgungViec { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ThoiGian_BatDau { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ThoiGian_KetThuc { get; set; }

        public decimal ThoiGian { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ThoiGian_XacNhan_BatDau { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ThoiGian_XacNhan_KetThuc { get; set; }

        public decimal? ThoiGian_XacNhan { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        public bool XacNhan { get; set; }
    }
}