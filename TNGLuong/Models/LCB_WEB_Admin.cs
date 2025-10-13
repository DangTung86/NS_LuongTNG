namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class LCB_WEB_Admin
    {
        [Key]
        [StringLength(14)]
        public string MaNS { get; set; }

        public bool KichHoat { get; set; }

        [StringLength(50)]
        public string VaiTro { get; set; }
    }
}