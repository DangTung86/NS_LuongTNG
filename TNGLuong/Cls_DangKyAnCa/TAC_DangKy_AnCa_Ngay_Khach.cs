namespace TNGLuong.Cls_DangKyAnCa
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAC_DangKy_AnCa_Ngay_Khach
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime Ngay { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DonViID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhongBanID { get; set; }

        public int? SoLuong { get; set; }
    }
}
