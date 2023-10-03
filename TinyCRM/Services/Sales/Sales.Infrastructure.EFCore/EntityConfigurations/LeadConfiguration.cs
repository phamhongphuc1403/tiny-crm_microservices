using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Infrastructure.EFCore.EntityConfigurations;

public class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.Property(p => p.Title).HasMaxLength(256);
        builder.Property(p => p.Description).HasMaxLength(1024);
        builder.Property(p => p.EstimatedRevenue).HasColumnType("decimal(18,2)").IsRequired();
        builder.HasOne(a => a.Customer)
            .WithMany()
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}