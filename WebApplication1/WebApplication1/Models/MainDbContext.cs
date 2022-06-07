using Microsoft.EntityFrameworkCore;
using System;

namespace WebApplication1.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MainDbContext()
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Musican> Musicans  { get; set; }
        public DbSet<Musican_Track> Musican_Tracks  { get; set; }
        public DbSet<MusicLabel> MusicLabels  { get; set; }
        public DbSet<Track> Tracks  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Album>(a =>
            {
                a.HasKey(e => e.IdAlbum);
                a.Property(e => e.AlbumName).IsRequired().HasMaxLength(30);
                a.Property(e => e.PublishDate).IsRequired();
                a.HasOne(e => e.MusicLabel).WithMany(e => e.Albums).HasForeignKey(e => e.IdMusicLabel);

                a.HasData(new Album { IdAlbum = 1, AlbumName = "NazwaAlbumu", PublishDate = DateTime.Parse("2000-03-04"),IdMusicLabel = 1});
                a.HasData(new Album { IdAlbum = 2, AlbumName = "Album", PublishDate = DateTime.Parse("2020-07-07"), IdMusicLabel = 2 });
            });

            modelBuilder.Entity<Musican>(m =>
            {
                m.HasKey(e => e.IdMusican);
                m.Property(e => e.FirstName).IsRequired().HasMaxLength(30);
                m.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                m.Property(e => e.NickName).HasMaxLength(20);

                m.HasData(new Musican {IdMusican = 1, FirstName = "Czarek", LastName = "Rozwadowski", NickName = "abc" });
                m.HasData(new Musican { IdMusican = 2, FirstName = "Jan", LastName = "Kowalski" });
            });

            modelBuilder.Entity<Musican_Track>(m =>
            {
                m.HasKey(e => new {e.IdMusican, e.IdTrack});
                m.HasOne(e => e.Musican).WithMany(e => e.Musican_Track).HasForeignKey(e => e.IdMusican);
                m.HasOne(e => e.Track).WithMany(e => e.Musican_Track).HasForeignKey(e => e.IdTrack);

                m.HasData(new Musican_Track { IdMusican = 1, IdTrack = 1});
                m.HasData(new Musican_Track { IdMusican = 2, IdTrack = 2 });
            });

            modelBuilder.Entity<MusicLabel>(m =>
            {
                m.HasKey(e => e.IdMusicLabel);
                m.Property(e => e.Name).IsRequired().HasMaxLength(50);

                m.HasData(new MusicLabel { IdMusicLabel = 1, Name = "NAzwaWytorwni"});
                m.HasData(new MusicLabel { IdMusicLabel = 2, Name = "Wytwornia" });
            });


            modelBuilder.Entity<Track>(t =>
            {
                t.HasKey(e =>e.IdTrack);
                t.Property(e => e.TrackName).IsRequired().HasMaxLength(20);
                t.Property(e => e.Duration).IsRequired();
                t.Property(e => e.IdMusicAlbum);
                t.HasOne(e => e.Album).WithMany(e => e.Tracks).HasForeignKey(e => e.IdMusicAlbum);

                t.HasData(new Track { IdTrack = 1, TrackName = "NazwaKawalka", Duration = 3f, IdMusicAlbum = 1});
                t.HasData(new Track { IdTrack = 2, TrackName = "NameOfTrack", Duration = 5f, IdMusicAlbum = 2 });
            });
        }
    }
}
