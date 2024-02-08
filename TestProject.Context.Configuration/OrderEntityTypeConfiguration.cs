using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestProject.Context.Contracts.Models;

namespace TestProject.Context.Configuration
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Number);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UpdatedAt).IsRequired();
            builder.Property(x => x.UpdatedBy).IsRequired().HasMaxLength(200);
            builder.Property(x => x.SenderCity).IsRequired().HasMaxLength(40);
            builder.Property(x => x.RecipientCity).IsRequired().HasMaxLength(40);
            builder.Property(x => x.SenderAddress).IsRequired().HasMaxLength(200);
            builder.Property(x => x.RecipientAddress).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Weight).IsRequired();
            builder.Property(x => x.PickupDate).IsRequired();
        }
    }
}
