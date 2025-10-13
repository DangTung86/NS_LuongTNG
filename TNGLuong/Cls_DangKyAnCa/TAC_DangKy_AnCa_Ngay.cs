namespace TNGLuong.Cls_DangKyAnCa
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAC_DangKy_AnCa_Ngay
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNS_ID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime Ngay { get; set; }

        public bool AnCa { get; set; }

        [StringLength(250)]
        public string GhiChu { get; set; }
    }
}
