using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.DealAggregate;

namespace Sales.Infrastructure.EFCore.EntityConfigurations;

public class DealLineConfiguration : IEntityTypeConfiguration<DealLine>
{
    public void Configure(EntityTypeBuilder<DealLine> builder)
    {
        builder.Property(dl => dl.Code).HasMaxLength(50).IsRequired().IsUnicode(false);
        builder.Property(dl => dl.Price).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(dl => dl.Quantity).IsRequired();
        builder.Property(dl => dl.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();

        builder.HasIndex(dl => dl.Code).IsUnique();

        builder.HasOne(dl => dl.Product)
            .WithMany()
            .HasForeignKey(dl => dl.ProductId)
            .OnDelete(DeleteBehavior
                .Cascade);
    }
}