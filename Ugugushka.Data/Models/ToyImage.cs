using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ugugushka.Data.Models
{
    public class ToyImage
    {
        public int ToyId { get; set; }
        public string PublicId { get; set; }
        public bool IsMain { get; set; }
        public string Format { get; set; }
    }

    internal class ImageConfiguration : IEntityTypeConfiguration<ToyImage>
    {
        public void Configure(EntityTypeBuilder<ToyImage> builder)
        {
            builder.HasKey(x => x.PublicId);
            builder.Property(x => x.PublicId).HasMaxLength(450);
            builder.Property(x => x.Format).HasMaxLength(10);
        }
    }
}
