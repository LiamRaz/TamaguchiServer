using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TamaguchiBL.Models
{
    public partial class TamaguchiContext : DbContext
    {
        public TamaguchiContext()
        {
        }

        public TamaguchiContext(DbContextOptions<TamaguchiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivitiesHistory> ActivitiesHistories { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<HealthStatus> HealthStatuses { get; set; }
        public virtual DbSet<LifeCycle> LifeCycles { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivitiesHistory>(entity =>
            {
                entity.HasKey(e => new { e.PetCode, e.ActivityDate });

                entity.Property(e => e.ActivityDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ActivityCodeNavigation)
                    .WithMany(p => p.ActivitiesHistories)
                    .HasForeignKey(d => d.ActivityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityHistory_Activities");

                entity.HasOne(d => d.PetCodeNavigation)
                    .WithMany(p => p.ActivitiesHistories)
                    .HasForeignKey(d => d.PetCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityHistory_Pets");

                entity.HasOne(d => d.PetHealthStatusNavigation)
                    .WithMany(p => p.ActivitiesHistories)
                    .HasForeignKey(d => d.PetHealthStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivitiesHistory_HealthStatus");

                entity.HasOne(d => d.PlayerCodeNavigation)
                    .WithMany(p => p.ActivitiesHistories)
                    .HasForeignKey(d => d.PlayerCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityHistory_Players");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.ActivityCode)
                    .HasName("PK__Activiti__2D7E17A6E80DEAF1");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activities_Categories");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).ValueGeneratedNever();
            });

            modelBuilder.Entity<HealthStatus>(entity =>
            {
                entity.HasKey(e => e.HealthCode)
                    .HasName("PK__HealthSt__6751E43EC39ED089");

                entity.Property(e => e.HealthCode).ValueGeneratedNever();
            });

            modelBuilder.Entity<LifeCycle>(entity =>
            {
                entity.HasKey(e => e.LifeCycleCode)
                    .HasName("PK__LifeCycl__FD63A3BAF5F2E690");

                entity.Property(e => e.LifeCycleCode).ValueGeneratedNever();
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(e => e.PetCode)
                    .HasName("PK__Pets__21F6A49BB9AA5DFD");

                entity.Property(e => e.BirthDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.HealthCodeNavigation)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.HealthCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pets_Health");

                entity.HasOne(d => d.LifeCycleCodeNavigation)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.LifeCycleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pets_LifeCycle");

                entity.HasOne(d => d.PlayerCodeNavigation)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.PlayerCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pets_Players");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.PlayerCode)
                    .HasName("PK__Players__EBD233B357328DDF");

                entity.HasIndex(e => e.CurrentPetId, "idx_CurrentPetID")
                    .IsUnique()
                    .HasFilter("([CurrentPetID] IS NOT NULL)");

                entity.HasOne(d => d.CurrentPet)
                    .WithOne(p => p.Player)
                    .HasForeignKey<Player>(d => d.CurrentPetId)
                    .HasConstraintName("FK_Players_Pets");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
