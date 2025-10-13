namespace TNGLuong.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;

    public partial class TNG_CTLDbContact : DbContext
    {
        public TNG_CTLDbContact()
            : base("name=TNG_CTLDbContact")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 30;
        }

        public virtual DbSet<LCB_KeHoach_NhanVien> LCB_KeHoach_NhanVien { get; set; }
        public virtual DbSet<LCB_KeHoach_NhanVien_ToTruongPhanCong> LCB_KeHoach_NhanVien_ToTruongPhanCong { get; set; }
        public virtual DbSet<LCB_MaHang> LCB_MaHang { get; set; }
        public virtual DbSet<LCB_NangSuatCongNhan> LCB_NangSuatCongNhan { get; set; }
        public virtual DbSet<LCB_NhayKhauCongNhan> LCB_NhayKhauCongNhan { get; set; }
        public virtual DbSet<View_ChiNhanh> View_ChiNhanh { get; set; }
        public virtual DbSet<View_ToMay> View_ToMay { get; set; }
        public virtual DbSet<LCB_KeHoach_To> LCB_KeHoach_To { get; set; }
        public virtual DbSet<LCB_NhayKhau> LCB_NhayKhau { get; set; }
        public virtual DbSet<LCB_LuongNgayCongNhan> LCB_LuongNgayCongNhan { get; set; }
        public virtual DbSet<LCB_DM_LyDoNgungViec> LCB_DM_LyDoNgungViec { get; set; }
        public virtual DbSet<LCB_ThoiGianNgungViec> LCB_ThoiGianNgungViec { get; set; }
        public virtual DbSet<LCB_PhanCumTruong> LCB_PhanCumTruong { get; set; }
        public virtual DbSet<LCB_WEB_TimeOpen> LCB_WEB_TimeOpen { get; set; }
        public virtual DbSet<LCB_WEB_Admin> LCB_WEB_Admin { get; set; }
        public virtual DbSet<LCB_WEB_ResetPass> LCB_WEB_ResetPass { get; set; }
        public virtual DbSet<LCB_MaHang_KhongBat_LuyKeBTP> LCB_MaHang_KhongBat_LuyKeBTP { get; set; }
        public virtual DbSet<LCB_ToTruong_CBC> LCB_ToTruong_CBC { get; set; }
        public virtual DbSet<LCB_WEB_KhoaBangLuong> LCB_WEB_KhoaBangLuong { get; set; }
        public virtual DbSet<LCB_ThongSoHeThong> LCB_ThongSoHeThong { get; set; }
        public virtual DbSet<LCB_WEB_ChucNang> LCB_WEB_ChucNang { get; set; }
        public virtual DbSet<LCB_WEB_KhoaChucNang> LCB_WEB_KhoaChucNang { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LCB_MaHang>()
                .Property(e => e.NangSuatBQ)
                .HasPrecision(8, 1);

            modelBuilder.Entity<LCB_MaHang>()
                .Property(e => e.HS_NgayCuoi)
                .HasPrecision(6, 4);

            modelBuilder.Entity<LCB_MaHang>()
                .Property(e => e.HS_BinhQuan)
                .HasPrecision(6, 4);

            modelBuilder.Entity<LCB_MaHang>()
                .Property(e => e.TongLuongCB)
                .HasPrecision(9, 0);

            modelBuilder.Entity<LCB_MaHang>()
                .Property(e => e.DonGiaTKC)
                .HasPrecision(12, 2);

            modelBuilder.Entity<LCB_MaHang>()
                .Property(e => e.K1)
                .HasPrecision(6, 4);
            modelBuilder.Entity<LCB_KeHoach_To>()
                .Property(e => e.HieuSuat)
                .HasPrecision(4, 2);

            modelBuilder.Entity<LCB_LuongNgayCongNhan>()
                .Property(e => e.GioLamViec)
                .HasPrecision(3, 1);

            modelBuilder.Entity<LCB_LuongNgayCongNhan>()
                .Property(e => e.LuongCB)
                .HasPrecision(9, 0);

            modelBuilder.Entity<LCB_LuongNgayCongNhan>()
                .Property(e => e.NangSuat)
                .HasPrecision(9, 0);

            modelBuilder.Entity<LCB_LuongNgayCongNhan>()
                .Property(e => e.NhayKhau)
                .HasPrecision(9, 0);

            modelBuilder.Entity<LCB_LuongNgayCongNhan>()
                .Property(e => e.LuongSP)
                .HasPrecision(9, 0);

            modelBuilder.Entity<LCB_LuongNgayCongNhan>()
                .Property(e => e.VuotNangSuat)
                .HasPrecision(9, 0);

            modelBuilder.Entity<LCB_LuongNgayCongNhan>()
                .Property(e => e.LuongThemGio)
                .HasPrecision(9, 0);

            modelBuilder.Entity<LCB_LuongNgayCongNhan>()
                .Property(e => e.LuongChoViec)
                .HasPrecision(9, 0);

            modelBuilder.Entity<LCB_LuongNgayCongNhan>()
                .Property(e => e.TongTienLuong)
                .HasPrecision(9, 0);

            modelBuilder.Entity<LCB_ThoiGianNgungViec>()
                .Property(e => e.ThoiGian)
                .HasPrecision(4, 1);

            modelBuilder.Entity<LCB_ThoiGianNgungViec>()
                .Property(e => e.ThoiGian_XacNhan)
                .HasPrecision(4, 1);

            modelBuilder.Entity<LCB_WEB_Admin>()
                .Property(e => e.MaNS)
                .IsUnicode(false);

            modelBuilder.Entity<LCB_WEB_Admin>()
                .Property(e => e.VaiTro)
                .IsUnicode(false);

            modelBuilder.Entity<LCB_WEB_ResetPass>()
                .Property(e => e.MaNS)
                .IsUnicode(false);

            modelBuilder.Entity<LCB_WEB_ResetPass>()
                .Property(e => e.MaNS_Reset)
                .IsUnicode(false);

            modelBuilder.Entity<LCB_ToTruong_CBC>()
                .Property(e => e.TyLe)
                .HasPrecision(8, 6);

            modelBuilder.Entity<LCB_WEB_KhoaBangLuong>()
                .Property(e => e.NguoiKhoa)
                .IsUnicode(false);

            modelBuilder.Entity<LCB_ThongSoHeThong>()
                .Property(e => e.GiaTri_So)
                .HasPrecision(18, 5);            
        }
    }
}
