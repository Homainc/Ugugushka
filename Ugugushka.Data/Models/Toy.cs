using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ugugushka.Data.Models
{
    public class Toy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public bool IsOnStock => Count > 0;
        public ISet<ToyImage> Images { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }

    internal class ToyConfiguration : IEntityTypeConfiguration<Toy>
    {
        public void Configure(EntityTypeBuilder<Toy> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .HasMaxLength(50);
            builder.Property(x => x.Description)
                .HasMaxLength(500);
            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Images)
                .WithOne()
                .HasForeignKey(x => x.ToyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(x => x.IsOnStock);
        }
    }
}
