using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MsSql.Context_For_Scaffolding.Models;

namespace MsSql.Context_For_Scaffolding.Data
{
    public partial class SocialNetworkContext : DbContext
    {
        public SocialNetworkContext()
        {
        }

        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountRoles> AccountRoles { get; set; }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<DbScriptMigration> DbScriptMigration { get; set; }
        public virtual DbSet<Entities> Entities { get; set; }
        public virtual DbSet<EntityTypes> EntityTypes { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserAccounts> UserAccounts { get; set; }
        public virtual DbSet<UserEntities> UserEntities { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=DESKTOP-L1PB0SE;initial catalog=SocialNetwork;persist security info=True;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRoles>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.RoleId });

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountRoles)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountRoles_Accounts");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AccountRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountRoles_Roles");
            });

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DbScriptMigration>(entity =>
            {
                entity.HasKey(e => e.MigrationId);

                entity.Property(e => e.MigrationId).ValueGeneratedNever();

                entity.Property(e => e.MigrationDate).HasColumnType("datetime");

                entity.Property(e => e.MigrationName)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Entities>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Properties).HasMaxLength(2000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Entities)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entities_EntityTypes");
            });

            modelBuilder.Entity<EntityTypes>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserAccounts>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AccountId });

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccounts_Accounts");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccounts_Users");
            });

            modelBuilder.Entity<UserEntities>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.EntityId });

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.UserEntities)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserEntities_Entities");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserEntities)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserEntities_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.LastAccess).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Properties).HasMaxLength(2000);

                entity.Property(e => e.Surname).HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
