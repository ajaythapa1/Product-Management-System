using Ajay.PMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ajay.PMS.ModelConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ProductName)
               .HasMaxLength(200)
               .IsUnicode(true);

            builder.Property(e => e.Manufacturer)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(e => e.Rate)
                .HasMaxLength(200)
                .IsUnicode(true);
            
            builder.Property(e => e.BatchNo)
                .HasMaxLength (200)
                .IsUnicode(true);

            builder.Property(e => e.ProductUrl)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(e => e.ProductionDate)
                .IsRequired()
           .HasColumnType("datetime");

            builder.Property(e => e.ExpiryDate)
                .IsRequired()
          .HasColumnType("datetime");

            builder.HasOne(e => e.Category)
              .WithMany(pt => pt.Products)
              .HasForeignKey(e => e.CategoryId);

            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .IsUnicode(true);

            builder.Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetDate()");

            builder.Property(e => e.ModifiedBy)
                .IsUnicode(true);

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime");

        }
    }
}
