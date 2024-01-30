using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Winterhold.DataAccess.Models;

public partial class WinterholdContext : DbContext
{
    public WinterholdContext()
    {
    }

    public WinterholdContext(DbContextOptions<WinterholdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-RB6U41HB;Initial Catalog=Winterhold;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("Account");

            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.DeceasedDate).HasColumnType("date");
            entity.Property(e => e.DeleteDate).HasColumnType("date");
            entity.Property(e => e.Education)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Summary)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Book");

            entity.Property(e => e.DeleteDate).HasColumnType("date");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReleaseDate).HasColumnType("date");
            entity.Property(e => e.Summary)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Author");

            entity.HasOne(d => d.CategoryNameNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Category");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("Category");

            entity.Property(e => e.DeleteDate).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Bay)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Isle)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.MembershipNumber);

            entity.ToTable("Customer");

            entity.Property(e => e.DeleteDate).HasColumnType("date");
            entity.Property(e => e.MembershipNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MembershipExpireDate).HasColumnType("date");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.ToTable("Loan");

            entity.Property(e => e.BookCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CustomerNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.LoanDate).HasColumnType("date");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ReturnDate).HasColumnType("date");

            entity.HasOne(d => d.BookCodeNavigation).WithMany(p => p.Loans)
                .HasForeignKey(d => d.BookCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_Book");

            entity.HasOne(d => d.CustomerNumberNavigation).WithMany(p => p.Loans)
                .HasForeignKey(d => d.CustomerNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_Customer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
