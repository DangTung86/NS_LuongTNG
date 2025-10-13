namespace TNGLuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NSTL_NS_ThongTinNS
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string MaNS_Old { get; set; }

        [StringLength(14)]
        public string MaNS { get; set; }

        [StringLength(20)]
        public string HoDem { get; set; }

        [StringLength(50)]
        public string Ten { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(20)]
        public string SoCMT { get; set; }

        public DateTime? NgayCap { get; set; }

        [StringLength(50)]
        public string NoiCap { get; set; }

        [StringLength(255)]
        public string NoiSinh { get; set; }

        public int? NguyenQuanTinh { get; set; }

        public int? NguyenQuanHuyen { get; set; }

        public int? HKTTTinh { get; set; }

        public int? HKTTHuyen { get; set; }

        public int? HKTTXa { get; set; }

        [StringLength(255)]
        public string HKTTXom { get; set; }

        [StringLength(255)]
        public string DiaChiTT { get; set; }

        public int? ThanhPhoID { get; set; }

        public int? QuanID { get; set; }

        public int? PhuongID { get; set; }

        public int? DanTocID { get; set; }

        public int? TonGiaoID { get; set; }

        public bool? GioiTinh { get; set; }

        public int? HocVanID { get; set; }

        public int? ChuyenNganhID_1 { get; set; }

        public int? ChuyenNganhID_2 { get; set; }

        public int? ChuyenNganhID_3 { get; set; }

        public int? TrinhDoVHID { get; set; }

        public DateTime? NgayDBDang { get; set; }

        [StringLength(14)]
        public string SoTheDang { get; set; }

        public DateTime? NgayVaoDangCT { get; set; }

        [StringLength(255)]
        public string NoiKetNap { get; set; }

        [StringLength(255)]
        public string NoiSinhHoatDang { get; set; }

        public DateTime? NgayVaoCD { get; set; }

        [StringLength(20)]
        public string TheCongDoan { get; set; }

        public int? GiaDinhCSID { get; set; }

        [StringLength(20)]
        public string SoBHXH { get; set; }

        public DateTime? NgayCapBHXH { get; set; }

        public DateTime? NgayThamGiaBHXH { get; set; }

        [StringLength(20)]
        public string SoBHYT { get; set; }

        [StringLength(255)]
        public string NoiDKKhamBenh { get; set; }

        public int? TrangThaiHD { get; set; }

        public int? NhomNguoiDungID { get; set; }

        public bool? LaNhanVien { get; set; }

        [StringLength(50)]
        public string NguoiTuyenDung { get; set; }

        public DateTime? NgayTuyenDung { get; set; }

        [StringLength(500)]
        public string Anh { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string DienThoai { get; set; }

        public int? LoaiNhanVien { get; set; }

        public bool? DaPhanBo { get; set; }

        [StringLength(255)]
        public string NguyenVong { get; set; }

        [StringLength(200)]
        public string GhiChuCongDoan { get; set; }

        public int? KhamChuaBenhID { get; set; }

        [StringLength(500)]
        public string LyLuanChinhTri { get; set; }

        [StringLength(500)]
        public string Quanlynhanuoc { get; set; }

        [StringLength(500)]
        public string NgoaiNgu { get; set; }

        [StringLength(500)]
        public string Tinhoc { get; set; }

        [StringLength(50)]
        public string ChieuCao { get; set; }

        [StringLength(50)]
        public string CanNang { get; set; }

        [StringLength(50)]
        public string NhomMau { get; set; }

        [StringLength(500)]
        public string TinhTrangSucKhoe { get; set; }

        [StringLength(500)]
        public string GhiChuLLTN3 { get; set; }

        [StringLength(50)]
        public string MaSoThe { get; set; }

        [StringLength(6)]
        public string MaSoCuoi { get; set; }

        public int? TinhThanhDOID { get; set; }

        public int? QuanDOID { get; set; }

        public int? PhuongDOID { get; set; }

        [StringLength(500)]
        public string XomDO { get; set; }

        public int? TinhThanhBTID { get; set; }

        public int? QuanBTID { get; set; }

        public int? PhuongBTID { get; set; }

        [StringLength(500)]
        public string XomBT { get; set; }

        public int? TinhTrangHonNhan { get; set; }

        public bool? TinhTrangGiaDinh { get; set; }

        public int? NamSinhCon { get; set; }

        public int? KenhTuyenDung { get; set; }

        [StringLength(50)]
        public string NguoiGioiThieu { get; set; }

        [StringLength(255)]
        public string NoiONguoiGT { get; set; }

        [StringLength(255)]
        public string DonViCu { get; set; }

        [StringLength(255)]
        public string ViTriCV { get; set; }

        [StringLength(255)]
        public string ThoiGianLV { get; set; }

        public decimal? MucLuong { get; set; }

        [StringLength(50)]
        public string BiDanh { get; set; }

        public int? NguyenVong2 { get; set; }

        public bool? DeNghiTL { get; set; }

        public bool? LoaiBo { get; set; }

        public DateTime? NgayDeNghiTL { get; set; }

        public DateTime? NgayVaoCongTy { get; set; }

        [StringLength(50)]
        public string MaSoThueCN { get; set; }

        [StringLength(50)]
        public string TheATM { get; set; }

        public int? NganHangID { get; set; }

        [StringLength(100)]
        public string NguoiGiamHo { get; set; }

        public bool? DuocBinhChon { get; set; }
    }
}
