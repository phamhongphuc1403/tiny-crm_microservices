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

        // builder.HasOne(a => a.Account)
        //     .WithMany(l => l.Leads)
        //     .HasForeignKey(d => d.AccountId)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Lead
            {
                Id = Guid.Parse("F90002E7-CB59-49D0-A7BA-3FFEF14E1F9A"),
                Title = "Et voluptatem sunt.",
                Source = LeadSource.Event,
                CustomerId = Guid.Parse("20419E47-058B-45AE-ADC7-A1DA89BB050C"),
                EstimatedRevenue = 73890,
                Status = LeadStatus.Prospect,
                CreatedDate = DateTime.Parse("2023-09-15")
            },
            new Lead
            {
                Id = Guid.Parse("004A27CA-30B7-4CA0-8178-6DC2B8DF5EC6"),
                Title = "Quae et dolorem mollitia repellendus ut.",
                Source = LeadSource.Event,
                CustomerId = Guid.Parse("22D84515-5912-489B-B75C-249964F5A278"),
                EstimatedRevenue = 81034,
                Status = LeadStatus.Open,
                CreatedDate = DateTime.Parse("2023-09-15")
            },
            new Lead
            {
                Id = Guid.Parse("F90002E7-CB59-49D0-A7BA-3FFEF14E1F9B"),
                Title = "Officia voluptatem et.",
                Source = LeadSource.Event,
                CustomerId = Guid.Parse("20419E47-058B-45AE-ADC7-A1DA89BB050C"),
                EstimatedRevenue = 88782,
                Status = LeadStatus.Disqualify,
                CreatedDate = DateTime.Parse("2023-09-15"),
                Description =
                    "While the Panther received knife and fork with a little scream of laughter. 'Oh, hush!' the Rabbit say, 'A barrowful of WHAT?' thought Alice to herself, (not in a moment to think that proved it at.",
                DisqualificationDate = DateTime.Parse("2023-09-16"),
                DisqualificationReason = LeadDisqualificationReason.Budget
            },
            new Lead
            {
                Id = Guid.Parse("004A27CA-30B7-4CA0-8178-6DC2B8DF5EC7"),
                Title = "Et voluptatem sunt.",
                Source = LeadSource.Event,
                CustomerId = Guid.Parse("22D84515-5912-489B-B75C-249964F5A278"),
                EstimatedRevenue = 72346,
                Status = LeadStatus.Qualify,
                CreatedDate = DateTime.Parse("2023-09-15"),
                Description =
                    "Alice, 'and why it is to-day! And I declare it's too bad, that it was only sobbing,' she thought, 'it's sure to make out what it was: at first was moderate. But the insolence of his."
            }
        );
    }
}