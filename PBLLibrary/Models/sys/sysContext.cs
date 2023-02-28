using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;



namespace PBLLibrary.Models.sys
{
    public partial class sysContext : DbContext
    {
        public sysContext()
        {
        }

        public sysContext(DbContextOptions<sysContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblEdict> TblEdict { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;UID=root;PWD=Nhant3019.;Port=3306;Database=sys");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblEdict>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbl_edict");

                entity.Property(e => e.Detail)
                    .HasColumnName("detail")
                    .HasColumnType("longtext");

                entity.Property(e => e.Idx)
                    .HasColumnName("idx")
                    .HasColumnType("int(255)");

                entity.Property(e => e.Word)
                    .HasColumnName("word")
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
