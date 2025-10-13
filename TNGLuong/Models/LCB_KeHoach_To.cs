namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_KeHoach_To
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
        [Column(Order = 4, TypeName = "date")]
        public DateTime Ngay { get; set; }

        public decimal HieuSuat { get; set; }

        public int KeHoach_To { get; set; }

        public int ThucHien_To { get; set; }

        public bool? IsEdit { get; set; }
    }
}