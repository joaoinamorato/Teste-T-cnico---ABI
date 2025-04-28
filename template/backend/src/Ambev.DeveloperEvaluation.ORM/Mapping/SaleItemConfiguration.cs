using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(si => si.Id);
        builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.OwnsOne(si => si.Product, product =>
        {
            product.Property(p => p.Id)
                .HasColumnName("ProductId")
                .HasColumnType("uuid")
                .IsRequired();

            product.Property(p => p.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(100)
                .IsRequired();

            product.Property(p => p.UnitPrice)
                .HasColumnName("ProductUnitPrice")
                .IsRequired();
        });

        builder.Property(si => si.Quantity).IsRequired();
        builder.Property(si => si.DiscountPerecentage).IsRequired();

        builder.HasOne(si => si.Sale)
            .WithMany(s => s.Items)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
