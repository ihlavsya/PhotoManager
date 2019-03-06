using PhotoManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoManager.DAL.EF
{
    public class PhotoManagerContext : DbContext
    {
        public PhotoManagerContext() : base("PhotoManagerContextDB")
        {
            Database.SetInitializer<PhotoManagerContext>(new PhotoManagerDBInitializer());
        }

        public DbSet<Photo> Photos { set; get; }
        public DbSet<Album> Albums { set; get; }
        public DbSet<User> Users { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)//for creating connection string you can use onmodelconfiguring
        {
            // use of Fluent API

            modelBuilder.Entity<User>().Property(p => p.Username).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Password).IsRequired();
            modelBuilder.Entity<User>().Property(a => a.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().HasKey(p => new { p.UserId });

            modelBuilder.Entity<Album>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Album>().Property(a => a.AlbumId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Album>().Property(p => p.GenresId).IsRequired();
            modelBuilder.Entity<Album>().Property(p => p.GenresId).HasColumnName("Genre");
            modelBuilder.Entity<Album>().Ignore(a => a.Genre);
            modelBuilder.Entity<Album>().HasKey(a => new { a.AlbumId });
            modelBuilder.Entity<Album>().HasIndex(a => a.UpdateDateTime);

            modelBuilder.Entity<Photo>().Property(p => p.PhotoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Photo>().Property(p => p.PhotoId).IsRequired();
            modelBuilder.Entity<Photo>().HasKey(p => new { p.PhotoId });
            modelBuilder.Entity<Photo>().HasIndex(p => p.UpdateDateTime);

            base.OnModelCreating(modelBuilder);
        }
    }
}
