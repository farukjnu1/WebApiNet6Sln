using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApiNet6.Models
{
    public partial class hospitaldbContext : DbContext
    {
        public hospitaldbContext()
        {
        }

        public hospitaldbContext(DbContextOptions<hospitaldbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Allergy> Allergies { get; set; }
        public virtual DbSet<AllergyDetail> AllergyDetails { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<Ncd> Ncds { get; set; }
        public virtual DbSet<NcdDetail> NcdDetails { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SERVERNAME,9433;Database=hospitaldb;User ID=ampletemp;Password=PASSWORD;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ampletemp")
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.ToTable("Allergy", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AllergyDetail>(entity =>
            {
                entity.ToTable("AllergyDetail", "dbo");

                entity.HasOne(d => d.Allergy)
                    .WithMany(p => p.AllergyDetails)
                    .HasForeignKey(d => d.AllergyId)
                    .HasConstraintName("FK_AllergyDetail_Allergy");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.AllergyDetails)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_AllergyDetail_Patient");
            });

            modelBuilder.Entity<Disease>(entity =>
            {
                entity.ToTable("Disease", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ncd>(entity =>
            {
                entity.ToTable("NCD", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NcdDetail>(entity =>
            {
                entity.ToTable("NcdDetail", "dbo");

                entity.HasOne(d => d.Ncd)
                    .WithMany(p => p.NcdDetails)
                    .HasForeignKey(d => d.NcdId)
                    .HasConstraintName("FK_NcdDetail_NCD");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.NcdDetails)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_NcdDetail_Patient");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient", "dbo");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Patient_Disease");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
