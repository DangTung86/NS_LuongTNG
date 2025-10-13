namespace TNGLuong.Cls_DangKyAnCa
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAC_DangKy_AnCa_NhanSu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNS_ID { get; set; }

        public bool DangKy_AnCa { get; set; }
    }
}
