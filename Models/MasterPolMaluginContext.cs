using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MasterPol.Models;

public partial class MasterPolMaluginContext : DbContext
{
    public MasterPolMaluginContext()
    {
    }

    public MasterPolMaluginContext(DbContextOptions<MasterPolMaluginContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerProduct> PartnerProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8SMGKI0;Database=MasterPol_Malugin;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.ToTable("Material_type");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.MaterialType1)
                .HasMaxLength(50)
                .HasColumnName("MaterialType");
            entity.Property(e => e.PercentageOfMaterialDefects).HasMaxLength(50);
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.IdPartner);

            entity.Property(e => e.IdPartner).HasColumnName("ID_Partner");
            entity.Property(e => e.Director).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.LegalAddress).HasMaxLength(100);
            entity.Property(e => e.PartnerName)
                .HasMaxLength(50)
                .HasColumnName("Partner_Name");
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Tin).HasColumnName("TIN");
            entity.Property(e => e.TypePartner)
                .HasMaxLength(50)
                .HasColumnName("Type_Partner");
        });

        modelBuilder.Entity<PartnerProduct>(entity =>
        {
            entity.HasKey(e => e.IdPartnerProduct);

            entity.ToTable("Partner_products");

            entity.Property(e => e.IdPartnerProduct)
                .ValueGeneratedNever()
                .HasColumnName("ID_Partner_Product");

            entity.HasOne(d => d.PartnerNavigation).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.Partner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_products_Partners");

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.Product)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_products_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Article).HasName("PK_Products_1");

            entity.Property(e => e.Article).ValueGeneratedNever();
            entity.Property(e => e.MinimumCostPartner).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NameProduct).HasMaxLength(100);

            entity.HasOne(d => d.ProductTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Product_type");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.IdTypeProduct);

            entity.ToTable("Product_type");

            entity.Property(e => e.IdTypeProduct)
                .ValueGeneratedNever()
                .HasColumnName("ID_Type_Product");
            entity.Property(e => e.ProductTypeFactor).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductTypeName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
