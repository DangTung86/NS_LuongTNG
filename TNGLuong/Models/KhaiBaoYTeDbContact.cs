namespace TNGLuong.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;

    public partial class KhaiBaoYTeDbContact : DbContext
    {
        public KhaiBaoYTeDbContact()
            : base("name=KhaiBaoYTeDbContact")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 30;
        }

        public virtual DbSet<SK_KhaiBaoDinhKy> SK_KhaiBaoDinhKy { get; set; }
        public virtual DbSet<SK_DiaDiemDieuTri> SK_DiaDiemDieuTri { get; set; }
        public virtual DbSet<SK_NhomBenh> SK_NhomBenh { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
        }
    }
}
