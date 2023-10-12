using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Infrastructure.EFCore.EntityConfigurations;

public class DealConfiguration : IEntityTypeConfiguration<Deal>
{
    public void Configure(EntityTypeBuilder<Deal> builder)
    {
        builder.Property(p => p.Title).HasMaxLength(256);
        builder.Property(p => p.Description).HasMaxLength(1024);
        builder.Property(d => d.EstimatedRevenue).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(d => d.ActualRevenue).HasColumnType("decimal(18,2)").IsRequired();
        builder.HasOne(d => d.Customer)
            .WithMany()
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.Lead)
            .WithOne(a => a.Deal)
            .HasForeignKey<Deal>(d => d.LeadId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(d => d.DealLines)
            .WithOne()
            .HasForeignKey(dl => dl.DealId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}