namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_LuongNgayCongNhan
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNS_ID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime Ngay { get; set; }

        public int PhongBanID { get; set; }

        public decimal GioLamViec { get; set; }

        public decimal LuongCB { get; set; }

        public decimal NangSuat { get; set; }

        public decimal NhayKhau { get; set; }

        public decimal LuongSP { get; set; }

        public decimal VuotNangSuat { get; set; }

        public decimal LuongThemGio { get; set; }

        public decimal LuongChoViec { get; set; }

        public decimal TongTienLuong { get; set; }

        public bool PheDuyet { get; set; }
    }
}