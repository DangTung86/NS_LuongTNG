namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SK_NhomBenh
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string TenNhomBenh { get; set; }
        [StringLength(250)]
        public string MoTa { get; set; }
        public bool TrangThai { get; set; }
    }
}
