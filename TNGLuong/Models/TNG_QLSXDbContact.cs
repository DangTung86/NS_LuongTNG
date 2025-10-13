namespace TNGLuong.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;

    public partial class TNG_QLSXDbContact : DbContext
    {
        public TNG_QLSXDbContact()
            : base("name=TNG_QLSXDbContact")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 30;
        }

        public virtual DbSet<CBC_BaoNgungViec> CBC_BaoNgungViec { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
