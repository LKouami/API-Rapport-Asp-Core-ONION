using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rapport.Domaine.Models;

namespace Rapport.Infrastructure
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activite> Activite { get; set; }
        public virtual DbSet<Horaire> Horaire { get; set; }
        public virtual DbSet<MyUser> MyUser { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=RapportData;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activite>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("ACTIVITE");
            });

            modelBuilder.Entity<Horaire>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("HORAIRE");

                entity.Property(e => e.DateDuJourHor).HasColumnType("date");
            });

            modelBuilder.Entity<MyUser>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("U_User_Email")
                    .IsUnique();

                entity.HasIndex(e => e.UserLogin)
                    .HasName("U_User_Login")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(160)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(160)
                    .IsFixedLength();

               

                entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();

                

                entity.Property(e => e.Token).HasMaxLength(255);

                entity.Property(e => e.UserLogin)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength();

               
            });

            

            

         
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
