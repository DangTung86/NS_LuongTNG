namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_ThongSoHeThong
    {
        [Key]
        public byte ID_ThongSo { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten_ThongSo { get; set; }

        public decimal GiaTri_So { get; set; }
    }
}