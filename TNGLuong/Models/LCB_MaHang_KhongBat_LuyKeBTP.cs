namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_MaHang_KhongBat_LuyKeBTP
    {
        [Key]
        public int Id_BTP { get; set; }

        [Required]
        [StringLength(250)]
        public string MaHang { get; set; }

        public int PhongBanID { get; set; }
        public DateTime? Ngay_Lap { get; set; }
        public string Nguoi_Lap { get; set; }
        public DateTime? Ngay_CapNhat { get; set; }
        public string Nguoi_CapNhat { get; set; }
        public bool TonTai { get; set; }
    }
}