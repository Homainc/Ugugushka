using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ugugushka.Data.Models
{
    public class Partition
    {
        public uint Id { get; set; }
        public string Name { get; set; }
    }

    internal class PartitionConfiguration : IEntityTypeConfiguration<Partition>
    {
        public void Configure(EntityTypeBuilder<Partition> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
        }
    }
}
