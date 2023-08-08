using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MYWebApplication.Models
{
    public partial class EmployeeProjectContext : DbContext
    {
        public EmployeeProjectContext()
        {
        }

        public EmployeeProjectContext(DbContextOptions<EmployeeProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmployeeList> EmployeeList { get; set; }
        public virtual DbSet<LoginUser> LoginUser { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSqlLocalDb;Database=EmployeeProject;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeList>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoginUser>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__LoginUse__C9F284577F70B1A1");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
