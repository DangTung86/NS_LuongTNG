namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_ToTruong_CBC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhongBanID { get; set; }

        public int MaNS_ID { get; set; }

        public byte? Hang { get; set; }

        public decimal? TyLe { get; set; }

        public int? MaNS_ID_QLPX { get; set; }

        public string Nguoi_Tao { get; set; }
        public DateTime Ngay_Tao { get; set; }
    }
}