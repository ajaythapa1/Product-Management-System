using Ajay.PMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ajay.PMS.ModelConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey( x => x.Id );

            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

            builder.Property(x => x.CategoryName)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(x => x.Description)
                .HasMaxLength (200)
                .IsUnicode(true);

            builder.HasMany(x => x.Products)
                .WithOne(pt => pt.Category)
                .HasForeignKey(x => x.CategoryId);

            builder.Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .IsUnicode(true);

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime");

            builder.Property(e => e.ModifiedBy)
                .IsUnicode(true);


        }
    }
}
