namespace TNGLuong.Cls_DangKyAnCa
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAC_DonGia_AnCa
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DonViID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime NgayApDung { get; set; }

        public decimal DonGia_AnCa { get; set; }

        public decimal SoTien_HoTro { get; set; }

        public decimal SoTien_DuocHuong { get; set; }
    }
}
