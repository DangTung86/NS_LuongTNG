namespace TNGLuong.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;

    public partial class TNGLuongDbContact : DbContext
    {
        public TNGLuongDbContact()
            : base("name=TNGLuongDbContact")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 30;
        }

        public virtual DbSet<DM_TaiKhoan> DM_TaiKhoan { get; set; }
        public virtual DbSet<NSTL_NS_ThongTinNS> NSTL_NS_ThongTinNS { get; set; }
        public virtual DbSet<View_BangLuongTheoThang> View_BangLuongTheoThang { get; set; }
        public virtual DbSet<View_Web_ThongTinNS> View_Web_ThongTinNS { get; set; }
        public virtual DbSet<View_Web_NhanSuTheoPB> View_Web_NhanSuTheoPB { get; set; }
        public virtual DbSet<View_Web_NhanSuTheoPB_MaNS_ID> View_Web_NhanSuTheoPB_MaNS_ID { get; set; }
        public virtual DbSet<View_BangLuongTheoThang_DANHGIA> View_BangLuongTheoThang_DANHGIA { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DM_TaiKhoan>()
                .Property(e => e.MaNS)
                .IsUnicode(false);

            modelBuilder.Entity<DM_TaiKhoan>()
                .Property(e => e.SoCMT)
                .IsUnicode(false);

            modelBuilder.Entity<DM_TaiKhoan>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<DM_TaiKhoan>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<DM_TaiKhoan>()
                .Property(e => e.AvatarUrl)
                .IsUnicode(false);

            modelBuilder.Entity<DM_TaiKhoan>()
                .Property(e => e.ConfirmCode)
                .IsUnicode(false);

            modelBuilder.Entity<NSTL_NS_ThongTinNS>()
                .Property(e => e.MaNS)
                .IsUnicode(false);

            modelBuilder.Entity<NSTL_NS_ThongTinNS>()
                .Property(e => e.SoCMT)
                .IsUnicode(false);

            modelBuilder.Entity<NSTL_NS_ThongTinNS>()
                .Property(e => e.SoTheDang)
                .IsUnicode(false);

            modelBuilder.Entity<NSTL_NS_ThongTinNS>()
                .Property(e => e.TheCongDoan)
                .IsUnicode(false);

            modelBuilder.Entity<NSTL_NS_ThongTinNS>()
                .Property(e => e.SoBHXH)
                .IsUnicode(false);

            modelBuilder.Entity<NSTL_NS_ThongTinNS>()
                .Property(e => e.SoBHYT)
                .IsUnicode(false);

            modelBuilder.Entity<NSTL_NS_ThongTinNS>()
                .Property(e => e.NguoiTuyenDung)
                .IsUnicode(false);

            modelBuilder.Entity<NSTL_NS_ThongTinNS>()
                .Property(e => e.MaSoCuoi)
                .IsUnicode(false);

            modelBuilder.Entity<NSTL_NS_ThongTinNS>()
                .Property(e => e.MucLuong)
                .HasPrecision(18, 0);

            modelBuilder.Entity<View_Web_ThongTinNS>()
                .Property(e => e.MaNS)
                .IsUnicode(false);

            modelBuilder.Entity<View_Web_NhanSuTheoPB>()
                .Property(e => e.MaNS)
                .IsUnicode(false);

            modelBuilder.Entity<View_Web_NhanSuTheoPB>()
                .Property(e => e.TenDonVi)
                .IsFixedLength();
            modelBuilder.Entity<View_Web_NhanSuTheoPB_MaNS_ID>()
               .Property(e => e.MaNS)
               .IsUnicode(false);

            modelBuilder.Entity<View_Web_NhanSuTheoPB_MaNS_ID>()
                .Property(e => e.TenDonVi)
                .IsFixedLength();

        }
    }
}
