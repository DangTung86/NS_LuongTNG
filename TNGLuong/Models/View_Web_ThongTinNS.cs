namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_Web_ThongTinNS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNS_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(14)]
        public string MaNS { get; set; }

        [StringLength(71)]
        public string HoTen { get; set; }

        [Key]
        [Column(Order = 2)]
        public string PassWord { get; set; }

        public int? PhongBanID { get; set; }

        public int? ToMay { get; set; }

        public int? ToTruong { get; set; }
        public int? DonViIDCha { get; set; }

        public int? DonViID { get; set; }

        [StringLength(50)]
        public string TenDonVi { get; set; }

        [StringLength(500)]
        public string TenPhongban { get; set; }
        public int? DoiTuongID { get; set; }
        public string AvatarUrl { get; set; }
        public byte? ID_NhomCongViec { get; set; }
        public int IsHetHanHD { get; set; }
    }
}