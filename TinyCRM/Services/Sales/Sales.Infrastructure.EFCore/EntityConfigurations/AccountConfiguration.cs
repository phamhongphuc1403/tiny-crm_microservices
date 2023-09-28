using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.AccountAggregate;

namespace Sales.Infrastructure.EFCore.EntityConfigurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(account => account.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(a => a.Email).IsUnique();
    }
}