namespace TNGLuong.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;

    public partial class TNG_CTLChiNhanhContact : DbContext
    {
        public TNG_CTLChiNhanhContact(string stringConnection)
            : base(stringConnection)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 30;
        }

        public virtual DbSet<tblCTL_BangCongNgay> tblCTL_BangCongNgay { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
