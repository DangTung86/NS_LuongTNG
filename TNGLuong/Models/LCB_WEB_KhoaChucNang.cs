namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class LCB_WEB_KhoaChucNang
    {
        [Key]
        public int ID_KhoaChucNang { get; set; }

        public int DonViID { get; set; }

        public int ID_ChucNang { get; set; }
    }
}