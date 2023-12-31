using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using People.Domain.AccountAggregate.Entities;

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
            .HasMaxLength(320);
        builder.Property(account => account.Phone)
            .HasMaxLength(30);
        builder.Property(account => account.Address)
            .HasMaxLength(255);
        builder.Property(e => e.TotalSales)
            .IsRequired()
            .HasPrecision(10, 2)
            .HasDefaultValue(0);
    }
}