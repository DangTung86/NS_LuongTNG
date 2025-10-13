namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_BangLuongTheoThang_DANHGIA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaBangLuong_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string DonViID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string PhongBanID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNS_ID { get; set; }

        public int? Thang { get; set; }

        public int? Nam { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LoaiBangLuong { get; set; }

        public int? TrangThai { get; set; }
    }
}
