using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using People.Domain.ContactAggregate.Entities;

namespace People.Infrastructure.EFCore.EntityConfigurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(contact => contact.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(contact => contact.Email)
            .IsRequired()
            .HasMaxLength(320);
        builder.Property(contact => contact.Phone)
            .HasMaxLength(30);

        builder
            .HasOne(contact => contact.Account)
            .WithMany(account => account.Contacts)
            .HasForeignKey(contact => contact.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}