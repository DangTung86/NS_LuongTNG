namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_WEB_TimeOpen
    {
        [Key]
        public int ID_ThoiGian { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public DateTime CreateAt { get; set; }

        public int STT { get; set; }

        [Required]
        [StringLength(12)]
        public string CreateUs { get; set; }

        public bool IsActive { get; set; }
    }
}