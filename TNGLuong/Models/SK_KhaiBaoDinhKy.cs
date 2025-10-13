namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SK_KhaiBaoDinhKy
    {
        public int Id { get; set; }
        [Required]
        [StringLength(14)]
        public string MaNS { get; set; }

        [Required]
        public int MaNS_ID { get; set; }
        [Required]
        public int DonViID { get; set; }
        [Required]
        public int PhongBanID { get; set; }
        [Required]
        public int IdNhomBenh { get; set; }
        [Required]
        public int IdNoiDieuTri { get; set; }

        [Required]
        [StringLength(500)]
        public string TenBenh { get; set; }

        public string PhuongPhapDieuTri { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayKhaiBao { get; set; }

        public string GhiChu { get; set; }

        public int Nam { get; set; }

        public string HoTen { get; set; }

        public bool KetQuaDieuTri { get; set; }
    }
}
