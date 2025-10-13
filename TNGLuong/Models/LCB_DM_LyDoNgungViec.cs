namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_DM_LyDoNgungViec
    {
        [Key]
        public byte ID_LyDoNgungViec { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten_LyDoNgungViec { get; set; }

        public int STT { get; set; }
    }
}