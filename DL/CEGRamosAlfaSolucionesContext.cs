using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class CEGRamosAlfaSolucionesContext : DbContext
    {
        public CEGRamosAlfaSolucionesContext()
        {
        }

        public CEGRamosAlfaSolucionesContext(DbContextOptions<CEGRamosAlfaSolucionesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<Beca> Becas { get; set; } = null!;
        public virtual DbSet<Materium> Materia { get; set; } = null!;
        public virtual DbSet<Semestre> Semestres { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database= CEGRamosAlfaSoluciones; Trusted_Connection=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno)
                    .HasName("PK__Alumno__460B47409A8F462E");

                entity.ToTable("Alumno");

                entity.Property(e => e.ApellidoMat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fotografia).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdBecaNavigation)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.IdBeca)
                    .HasConstraintName("FK__Alumno__IdBeca__182C9B23");
            });

            modelBuilder.Entity<Beca>(entity =>
            {
                entity.HasKey(e => e.IdBeca)
                    .HasName("PK__Beca__23D228E04A146072");

                entity.ToTable("Beca");

                entity.Property(e => e.MontoMensual).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.NombreBeca)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Materium>(entity =>
            {
                entity.HasKey(e => e.IdMateria)
                    .HasName("PK__Materia__EC174670A5608A6B");

                entity.Property(e => e.NombreMateria)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSemestreNavigation)
                    .WithMany(p => p.Materia)
                    .HasForeignKey(d => d.IdSemestre)
                    .HasConstraintName("FK__Materia__IdSemes__1367E606");
            });

            modelBuilder.Entity<Semestre>(entity =>
            {
                entity.HasKey(e => e.IdSemestre)
                    .HasName("PK__Semestre__BD1FD7F88947FD14");

                entity.ToTable("Semestre");

                entity.Property(e => e.NombreSemestre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF974318A36D");

                entity.ToTable("Usuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
