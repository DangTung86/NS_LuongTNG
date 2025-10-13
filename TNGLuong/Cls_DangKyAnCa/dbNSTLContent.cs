namespace TNGLuong.Cls_DangKyAnCa
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbNSTLContent : DbContext
    {
        public dbNSTLContent()
            : base("name=dbNSTLContent")
        {
        }

        public virtual DbSet<TAC_DangKy_AnCa_Chot> TAC_DangKy_AnCa_Chot { get; set; }
        public virtual DbSet<TAC_DangKy_AnCa_Ngay> TAC_DangKy_AnCa_Ngay { get; set; }
        public virtual DbSet<TAC_DangKy_AnCa_Ngay_Khach> TAC_DangKy_AnCa_Ngay_Khach { get; set; }
        public virtual DbSet<TAC_DangKy_AnCa_NhanSu> TAC_DangKy_AnCa_NhanSu { get; set; }
        public virtual DbSet<TAC_DonGia_AnCa> TAC_DonGia_AnCa { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAC_DonGia_AnCa>()
                .Property(e => e.DonGia_AnCa)
                .HasPrecision(8, 0);

            modelBuilder.Entity<TAC_DonGia_AnCa>()
                .Property(e => e.SoTien_HoTro)
                .HasPrecision(8, 0);

            modelBuilder.Entity<TAC_DonGia_AnCa>()
                .Property(e => e.SoTien_DuocHuong)
                .HasPrecision(8, 0);
        }
    }
}
