namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_TaiKhoan
    {
        public int Id { get; set; }

        public int MaNS_ID { get; set; }

        [Required]
        [StringLength(14)]
        public string MaNS { get; set; }

        [Required]
        [StringLength(100)]
        public string HoDem { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime NgaySinh { get; set; }

        [Required]
        [StringLength(20)]
        public string SoCMT { get; set; }

        public bool GioiTinh { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        public string PassWord { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatePass { get; set; }

        [Column(TypeName = "image")]
        public byte[] Avatar { get; set; }

        public bool IsActive { get; set; }

        [StringLength(100)]
        public string AvatarUrl_old { get; set; }

        [StringLength(200)]
        public string CoverImage_old { get; set; }

        public bool? IsConfirmEmail { get; set; }

        [StringLength(10)]
        public string ConfirmCode { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ExpiryCode { get; set; }

        public bool? La_BaoVe { get; set; }

        public bool? La_KhachHang { get; set; }

        [StringLength(20)]
        public string MaChiNhanh { get; set; }

        public int? MaKhachHang { get; set; }

        [StringLength(100)]
        public string AvatarUrl { get; set; }

        [StringLength(200)]
        public string CoverImage { get; set; }
    }
}
