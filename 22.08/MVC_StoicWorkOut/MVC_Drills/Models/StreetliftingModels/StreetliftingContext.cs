using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVC_Drills.Models.StreetliftingModels;

public partial class StreetliftingContext : DbContext
{
    public StreetliftingContext()
    {
    }

    public StreetliftingContext(DbContextOptions<StreetliftingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BodyProgress> BodyProgresses { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<LeaderBoard> LeaderBoards { get; set; }

    public virtual DbSet<LiftProgress> LiftProgresses { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-E0P9L99\\SQLEXPRESS01;database=Streetlifting;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BodyProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BodyProg__3214EC272DE91F95");

            entity.ToTable("BodyProgress");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Photo1).HasMaxLength(255);
            entity.Property(e => e.Photo2).HasMaxLength(255);
            entity.Property(e => e.Photo3).HasMaxLength(255);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.BodyProgress)
                .HasForeignKey<BodyProgress>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BodyProgress__ID__44FF419A");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exercise__3214EC27844EE93C");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.CompoundExercise1).HasMaxLength(255);
            entity.Property(e => e.CompoundExercise2).HasMaxLength(255);
            entity.Property(e => e.CompoundExercise3).HasMaxLength(255);
            entity.Property(e => e.CompoundExercise4).HasMaxLength(255);
            entity.Property(e => e.CompoundExercise5).HasMaxLength(255);
            entity.Property(e => e.IsolationExercise1).HasMaxLength(255);
            entity.Property(e => e.IsolationExercise2).HasMaxLength(255);
            entity.Property(e => e.IsolationExercise3).HasMaxLength(255);
            entity.Property(e => e.IsolationExercise4).HasMaxLength(255);
            entity.Property(e => e.IsolationExercise5).HasMaxLength(255);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Exercise)
                .HasForeignKey<Exercise>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exercises__ID__47DBAE45");
        });

        modelBuilder.Entity<LeaderBoard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LeaderBo__3214EC27D5BFDB3C");

            entity.ToTable("LeaderBoard");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Comments).HasMaxLength(255);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.LeaderBoard)
                .HasForeignKey<LeaderBoard>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LeaderBoard__ID__4222D4EF");
        });

        modelBuilder.Entity<LiftProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LiftProg__3214EC27487FCD94");

            entity.ToTable("LiftProgress");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.LiftProgress)
                .HasForeignKey<LiftProgress>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LiftProgress__ID__3F466844");
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__measurem__3214EC275810B5AC");

            entity.ToTable("measurements");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Measurement)
                .HasForeignKey<Measurement>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__measurements__ID__3C69FB99");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC274CD6F5FD");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(255);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserProf__3214EC27A40C5CCA");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.SurName).HasMaxLength(255);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.UserProfile)
                .HasForeignKey<UserProfile>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserProfiles__ID__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
