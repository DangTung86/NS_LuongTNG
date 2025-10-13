namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_ChiNhanh
    {
        [Key]
        public int DonViID { get; set; }

        [StringLength(4000)]
        public string TenDonVi { get; set; }
    }
}