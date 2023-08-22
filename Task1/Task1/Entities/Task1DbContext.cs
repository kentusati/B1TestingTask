using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task1.Entities;

namespace Task1;

public partial class Task1DbContext : DbContext
{
    public Task1DbContext()
    {
    }

    public Task1DbContext(DbContextOptions<Task1DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TaskTable> TaskTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost; Database=Task1DB; Integrated Security=false; User Id=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskTable_pkey");

            entity.ToTable("tasktable");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
