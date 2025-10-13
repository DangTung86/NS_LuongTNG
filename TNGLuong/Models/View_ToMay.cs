namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_ToMay
    {
        [Key]
        public int PhongBanID { get; set; }

        [StringLength(500)]
        public string TenPhongban { get; set; }

        [StringLength(10)]
        public string DonViID { get; set; }
    }
}