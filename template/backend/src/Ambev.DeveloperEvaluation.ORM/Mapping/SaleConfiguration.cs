using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.OwnsOne(s => s.Branch, branch =>
        {
            branch.Property(b => b.Id)
                .HasColumnName("BranchId")
                .HasColumnType("uuid")
                .IsRequired();

            branch.Property(b => b.Name)
                .HasColumnName("BranchName")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(s => s.Customer, customer =>
        {
            customer.Property(c => c.Id)
                .HasColumnName("CustomerId")
                .HasColumnType("uuid")
                .IsRequired();

            customer.Property(c => c.Name)
                .HasColumnName("CustomerName")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.HasMany(s => s.Items)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
