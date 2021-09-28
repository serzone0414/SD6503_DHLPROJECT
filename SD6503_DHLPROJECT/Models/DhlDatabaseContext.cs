using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SD6503_DHLPROJECT.Models
{
    public partial class DhlDatabaseContext : DbContext
    {
        public DhlDatabaseContext()
        {
        }

        public DhlDatabaseContext(DbContextOptions<DhlDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDetail> AccountDetails { get; set; }
        public virtual DbSet<LoginAccount> LoginAccounts { get; set; }
        public virtual DbSet<TransactionTable> TransactionTables { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountDetail>(entity =>
            {
                entity.HasKey(e => e.AccountNumber)
                    .HasName("PK__AccountD__BE2ACD6E6CE2BFBE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.IdentifierNavigation)
                    .WithMany(p => p.AccountDetails)
                    .HasForeignKey(d => d.Identifier)
                    .HasConstraintName("FK__AccountDe__Ident__66603565");
            });

            modelBuilder.Entity<LoginAccount>(entity =>
            {
                entity.HasKey(e => e.Identifier)
                    .HasName("PK__LoginAcc__821FB018DAA7874C");

                entity.ToTable("LoginAccount");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TransactionTable>(entity =>
            {
                entity.HasKey(e => e.TransactionNumber)
                    .HasName("PK__Transact__E733A2BEC5338220");

                entity.ToTable("TransactionTable");

                entity.HasOne(d => d.FromAccountNavigation)
                    .WithMany(p => p.TransactionTables)
                    .HasForeignKey(d => d.FromAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__FromA__693CA210");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
