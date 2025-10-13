namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_NhayKhau
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNS_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhongBanID_NS { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime Ngay { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_CongDoan { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(250)]
        public string MaHang { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhongBanID { get; set; }

        [Key]
        [Column(Order = 6)]
        public byte NhomSize { get; set; }

        [Key]
        [Column(Order = 7)]
        public byte ID_CachMay { get; set; }

        public int SoLuong_NhayKhau { get; set; }
    }
}