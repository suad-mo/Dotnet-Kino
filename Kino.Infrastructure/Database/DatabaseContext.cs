using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Kino.Core.Entities;

namespace Kino.Infrastructure.Database
{
    public class DatabaseContext : IdentityDbContext<UserAccount>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Film> Film { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<Red> Red { get; set; }
        public DbSet<VrstaSjedista> VrstaSjedista { get; set; }
        public DbSet<Sjediste> Sjediste { get; set; }
        public DbSet<Projekcija> Projekcija { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<Licnost> Licnost { get; set; }
        public DbSet<FilmLicnost> FilmLicnost { get; set; }
        public DbSet<KategorijaArtikl> KategorijaArtikl { get; set; }
        public DbSet<Artikl> Artikl { get; set; }
        public DbSet<FilmTranslation> FilmTranslation { get; set; }
        public DbSet<SalaTranslation> SalaTranslation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Film>().Property(f => f.Jezik).HasConversion<string>();

            modelBuilder.Entity<FilmLicnost>()
                        .HasOne<Film>(fl => fl.Film)
                        .WithMany(f => f.Licnosti)
                        .HasForeignKey(fl => fl.FilmId);

            modelBuilder.Entity<FilmLicnost>()
                        .HasOne(fl => fl.Licnost)
                        .WithMany(l => l.Filmovi)
                        .HasForeignKey(fl => fl.LicnostId);

            modelBuilder.Entity<FilmTranslation>()
                        .HasKey(ft => new { ft.FilmId, ft.Language });

            modelBuilder.Entity<SalaTranslation>()
                        .HasKey(st => new { st.SalaId, st.Language });
        }
    }
}
