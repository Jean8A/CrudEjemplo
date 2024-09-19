using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EJEMPLO_CRUD_SP.Models;

public partial class UniversidadContext : DbContext
{
    public UniversidadContext()
    {
    }

    public UniversidadContext(DbContextOptions<UniversidadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Inscripcion> Inscripcions { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=MEDAPRCSGFSP680\\SQLEXPRESS;Initial Catalog=universidad; integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__estudian__E0B2763CC5FC946A");

            entity.ToTable("estudiantes");

            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("PK__inscripc__CB0117BA3DB75C56");

            entity.ToTable("inscripcion");

            entity.Property(e => e.IdInscripcion).HasColumnName("id_inscripcion");
            entity.Property(e => e.FechaInscripcion).HasColumnName("fecha_inscripcion");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdMateria).HasColumnName("id_materia");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("FK__inscripci__id_es__4D94879B");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.IdMateria)
                .HasConstraintName("FK__inscripci__id_ma__4E88ABD4");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PK__materias__7E03FD3973B51C63");

            entity.ToTable("materias");

            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreMateria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_materia");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
