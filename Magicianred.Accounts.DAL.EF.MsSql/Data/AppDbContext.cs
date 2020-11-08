using Magicianred.Accounts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Magicianred.Accounts.DAL.EF.MsSql.Data
{
    public class AppDbContext : DbContext
    {

        public virtual DbSet<AccountRole> AccountRoles { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityType> EntityTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserEntity> UserEntities { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRole>(entity =>
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

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<UserAccount>(entity =>
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id);
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

            modelBuilder.Entity<EntityType>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.Property(e => e.Id);

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

            modelBuilder.Entity<UserEntity>(entity =>
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

        }

    }
}