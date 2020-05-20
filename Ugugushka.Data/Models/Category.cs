using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ugugushka.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PartitionId { get; set; }
        public Partition Partition { get; set; }
    }

    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasOne(x => x.Partition)
                .WithMany()
                .HasForeignKey(x => x.PartitionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
