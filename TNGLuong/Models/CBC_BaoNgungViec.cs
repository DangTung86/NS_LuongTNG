namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CBC_BaoNgungViec
    {
        [Key]
        public int ID_BaoNgungViec { get; set; }

        [Required]
        [StringLength(20)]
        public string LyDoNgungViec { get; set; }

        [Required]
        [StringLength(14)]
        public string MaNS { get; set; }

        public DateTime ThoiGian_BatDau { get; set; }

        public DateTime? ThoiGian_KetThuc { get; set; }
    }
}
