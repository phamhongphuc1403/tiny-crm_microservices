using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Entities;

namespace BuildingBlock.Infrastructure.EFCore.EntityConfigurations;

public class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.Property(p => p.Title).HasMaxLength(256);
        builder.Property(p => p.Description).HasMaxLength(1024);
        builder.Property(p => p.EstimatedRevenue).HasColumnType("decimal(18,2)").IsRequired();

        // builder.HasOne(a => a.Account)
        //     .WithMany(l => l.Leads)
        //     .HasForeignKey(d => d.AccountId)
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}