namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class LCB_WEB_ChucNang
    {
        [Key]
        public int ID_ChucNang { get; set; }

        [Required]
        [StringLength(100)]
        public string TenChucNang { get; set; }

        public int ID_NhomChuNang { get; set; }

        [StringLength(50)]
        public string TenControl { get; set; }

        [StringLength(50)]
        public string TenControl_Mobile { get; set; }

        [StringLength(50)]
        public string TenControl_Login { get; set; }
    }
}