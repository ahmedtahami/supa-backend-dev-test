using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuizAPI.Models
{
    public partial class QuizDbContext : DbContext
    {
        public QuizDbContext()
        {
        }

        public QuizDbContext(DbContextOptions<QuizDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Content> Contents { get; set; } = null!;
        public virtual DbSet<Network> Networks { get; set; } = null!;
        public virtual DbSet<UserContent> UserContents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Content__0DC06F8CE3554BFD");

                entity.ToTable("Content");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.Answer1).HasMaxLength(20);

                entity.Property(e => e.Answer2).HasMaxLength(20);

                entity.Property(e => e.Answer3).HasMaxLength(20);

                entity.Property(e => e.Answer4).HasMaxLength(20);

                entity.Property(e => e.CorrectAnswer).HasMaxLength(20);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Content__Usernam__59063A47");
            });

            modelBuilder.Entity<Network>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Network__A25C5AA6D174131D");

                entity.ToTable("Network");

                entity.Property(e => e.Code)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.NetworkName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserContent>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__UserCont__F3DBC573082BC7B5");

                entity.ToTable("UserContent");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.DigitNumber).HasColumnName("digit_number");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Losses).HasColumnName("losses");

                entity.Property(e => e.Wins).HasColumnName("wins");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
