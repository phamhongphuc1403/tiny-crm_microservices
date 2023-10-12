using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Infrastructure.EFCore.EntityConfigurations;

public class DealLineConfiguration : IEntityTypeConfiguration<DealLine>
{
    public void Configure(EntityTypeBuilder<DealLine> builder)
    {
        builder.Property(dl => dl.PricePerUnit).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(dl => dl.Quantity).IsRequired();
        builder.Property(dl => dl.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();

        builder.HasOne(dl => dl.Product)
            .WithMany(product => product.DealLines)
            .HasForeignKey(dl => dl.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dl => dl.Deal)
            .WithMany(deal => deal.DealLines)
            .HasForeignKey(dl => dl.DealId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}