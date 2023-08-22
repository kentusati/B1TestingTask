using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task2.Entities;

//класс сгенерированый EFCore т.к. использовался подход DB-First
public partial class Task1DbContext : DbContext
{
    public Task1DbContext()
    {
    }

    public Task1DbContext(DbContextOptions<Task1DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Balance> Balances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost; Database=Task1DB;Integrated Security=false; User id=postgres; password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Balance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("input_balance_pkey");

            entity.ToTable("balance", "taskschem");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AccountNumber).HasColumnName("account_number");
            entity.Property(e => e.Credit)
                .HasColumnType("money")
                .HasColumnName("credit");
            entity.Property(e => e.Debit)
                .HasColumnType("money")
                .HasColumnName("debit");
            entity.Property(e => e.IbAssets)
                .HasColumnType("money")
                .HasColumnName("ib_assets");
            entity.Property(e => e.IbPassive)
                .HasColumnType("money")
                .HasColumnName("ib_passive");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
