using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using People.Domain.Entities;

namespace People.Infrastructure.EFCore.EntityConfigurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(account => account.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(a => a.Email).IsUnique();
        builder.Property(account => account.Email)
            .IsRequired()
            .HasMaxLength(320);
        builder.HasIndex(a => a.Phone).IsUnique();
        builder.Property(account => account.Phone)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(account => account.Address)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(e => e.TotalSales)
            .IsRequired()
            .HasPrecision(10, 2);
    }
}