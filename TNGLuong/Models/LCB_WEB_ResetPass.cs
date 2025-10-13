namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_WEB_ResetPass
    {
        [Key]
        public int ID_ResetPass { get; set; }

        [Required]
        [StringLength(14)]
        public string MaNS { get; set; }

        public DateTime NgayReset { get; set; }

        [Required]
        [StringLength(14)]
        public string MaNS_Reset { get; set; }
    }
}