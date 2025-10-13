namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_KeHoach_NhanVien_ToTruongPhanCong
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(250)]
        public string MaHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhongBanID { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte NhomSize { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte ID_CachMay { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_CongDoan { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNS_ID { get; set; }

       
        [Column(Order = 6, TypeName = "date")]
        public DateTime Ngay { get; set; }

        public int PhongBanID_NS { get; set; }

        public int KeHoach_NhanVien { get; set; }

        public int ThucHien_NhanVien { get; set; }
    }
}
