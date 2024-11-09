using System;
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

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductColor> ProductColors { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductPriceHistory> ProductPriceHistories { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=lllk21416c.database.windows.net;Database=TextMining;User Id=anhhoangdev;Password=kamadoJr1@;Encrypt=True;TrustServerCertificate=True;");

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

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.BrandName).HasMaxLength(128);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Asin);

            entity.ToTable("Product");

            entity.Property(e => e.Asin)
                .HasMaxLength(50)
                .HasColumnName("ASIN");
            entity.Property(e => e.OwnedBy).HasMaxLength(128);
            entity.Property(e => e.ProductName).HasMaxLength(512);

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Product_Brand");

            entity.HasOne(d => d.OwnedByNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.OwnedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_APP_Account");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_SubCategory");
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.ToTable("ProductColor");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Asin)
                .HasMaxLength(50)
                .HasColumnName("ASIN");
            entity.Property(e => e.ColorHex).HasMaxLength(10);

            entity.HasOne(d => d.AsinNavigation).WithMany(p => p.ProductColors)
                .HasForeignKey(d => d.Asin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductColor_Product");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.ToTable("ProductImage");

            entity.Property(e => e.Asin)
                .HasMaxLength(50)
                .HasColumnName("ASIN");

            entity.HasOne(d => d.AsinNavigation).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.Asin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImage_Product");
        });

        modelBuilder.Entity<ProductPriceHistory>(entity =>
        {
            entity.ToTable("ProductPriceHistory");

            entity.Property(e => e.Asin)
                .HasMaxLength(50)
                .HasColumnName("ASIN");

            entity.HasOne(d => d.AsinNavigation).WithMany(p => p.ProductPriceHistories)
                .HasForeignKey(d => d.Asin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductPriceHistory_Product");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Asin)
                .HasMaxLength(50)
                .HasColumnName("ASIN");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.OwnedBy).HasMaxLength(128);
            entity.Property(e => e.Title).HasMaxLength(500);

            entity.HasOne(d => d.AsinNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Asin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Product");

            entity.HasOne(d => d.OwnedByNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OwnedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_APP_Account");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.ToTable("SubCategory");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategory_Category");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.FirstName).IsUnicode(false);
            entity.Property(e => e.LastName).IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_APP_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
