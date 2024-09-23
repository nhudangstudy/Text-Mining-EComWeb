﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public partial class TextMiningDbContext : DbContext
{
    public TextMiningDbContext()
    {
    }

    public TextMiningDbContext(DbContextOptions<TextMiningDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppAccount> AppAccounts { get; set; }

    public virtual DbSet<AppAuthentication> AppAuthentications { get; set; }

    public virtual DbSet<AppRefreshToken> AppRefreshTokens { get; set; }

    public virtual DbSet<AppScope> AppScopes { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1443;Database=TextMining;User Id=sa;Password=LiveLaughLove@001;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppAccount>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK_Account");

            entity.ToTable("APP_Account");

            entity.Property(e => e.Email).HasMaxLength(128);
        });

        modelBuilder.Entity<AppAuthentication>(entity =>
        {
            entity.ToTable("App_Authentication");

            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.Expired).HasColumnType("datetime");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.AppAuthentications)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_App_Authentication_APP_Account");
        });

        modelBuilder.Entity<AppRefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("PK_APP_RefreshToken_1");

            entity.ToTable("APP_RefreshToken");

            entity.Property(e => e.RefreshTokenId).ValueGeneratedNever();
            entity.Property(e => e.AccountId).HasMaxLength(128);
            entity.Property(e => e.ExpiredTime).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.AppRefreshTokens)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APP_RefreshToken_APP_Account");
        });

        modelBuilder.Entity<AppScope>(entity =>
        {
            entity.ToTable("APP_Scope");

            entity.Property(e => e.Value).HasMaxLength(200);

            entity.HasMany(d => d.Accounts).WithMany(p => p.Scopes)
                .UsingEntity<Dictionary<string, object>>(
                    "AppAccountScope",
                    r => r.HasOne<AppAccount>().WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_APP_Account_Scope_APP_Account"),
                    l => l.HasOne<AppScope>().WithMany()
                        .HasForeignKey("ScopeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_APP_Account_Scope_APP_Scope"),
                    j =>
                    {
                        j.HasKey("ScopeId", "AccountId");
                        j.ToTable("APP_Account_Scope");
                        j.IndexerProperty<string>("AccountId").HasMaxLength(128);
                    });
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Test");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
