using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NoticiasApi.Database
{
    public partial class NoticiasContext : DbContext
    {
        public NoticiasContext()
        {
        }

        public NoticiasContext(DbContextOptions<NoticiasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Noticia> Noticia { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // To protect potentially sensitive information in your connection string, you should move it out of source code.
                //See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user=root;database=Noticias", x => x.ServerVersion("10.4.11-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("autor");

                entity.Property(e => e.AutorId)
                    .HasColumnName("autorId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnName("apellido")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_spanish_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_spanish_ci");
            });

            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.ToTable("noticia");

                entity.HasIndex(e => e.AutorId)
                    .HasName("autorId");

                entity.Property(e => e.NoticiaId)
                    .HasColumnName("noticiaId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AutorId)
                    .HasColumnName("autorId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Contenido)
                    .IsRequired()
                    .HasColumnName("contenido")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_spanish_ci");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_spanish_ci");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_spanish_ci");

                entity.HasOne(d => d.Autor)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.AutorId)
                    .HasConstraintName("autorId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
