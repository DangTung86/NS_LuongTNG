namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LCB_WEB_KhoaBangLuong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DonViID { get; set; }

        public bool KhoaBlg { get; set; }

        [Required]
        [StringLength(14)]
        public string NguoiKhoa { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime NgayKhoa { get; set; }

        public bool TonTai { get; set; }
    }
}