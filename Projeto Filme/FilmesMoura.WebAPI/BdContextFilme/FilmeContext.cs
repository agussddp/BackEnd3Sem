using System;
using System.Collections.Generic;
using FilmesMoura.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesMoura.WebAPI.BdContextFilme;

public partial class FilmeContext : DbContext
{
    public FilmeContext()
    {
    }

    public FilmeContext(DbContextOptions<FilmeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FIlme> FIlmes { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FilmesBd;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FIlme>(entity =>
        {
            entity.HasKey(e => e.IdFilme).HasName("PK__fIlme__6E8F2A769A753A23");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.FIlmes).HasConstraintName("FK__fIlme__IdGenero__5EBF139D");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F8349880B560D72");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97697F31E3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
