using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace login.Models
{
    public partial class dataofmedContext : DbContext
    {
        public dataofmedContext()
        {
        }

        public dataofmedContext(DbContextOptions<dataofmedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Invt> Invts { get; set; } = null!;
        public virtual DbSet<Typesmed> Typesmeds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=sql12.freesqldatabase.com;database=sql12749727;user=sql12749727;pwd=rTKkknie56", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.5.62-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Invt>(entity =>
            {
                entity.ToTable("invt");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Medicinetype)
                    .HasMaxLength(123)
                    .HasColumnName("medicinetype");

                entity.Property(e => e.Medis).HasMaxLength(150);

                entity.Property(e => e.Mendname).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("int(11)");

                entity.Property(e => e.Statusmed)
                    .HasMaxLength(23)
                    .HasColumnName("statusmed");

                entity.Property(e => e.Stock)
                    .HasColumnType("int(11)")
                    .HasColumnName("stock");

                entity.Property(e => e.Typemed).HasMaxLength(50);

                entity.Property(e => e.Userid).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Typesmed>(entity =>
            {
                entity.ToTable("typesmed");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(23);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
