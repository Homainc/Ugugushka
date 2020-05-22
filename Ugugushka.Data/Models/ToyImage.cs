using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ugugushka.Data.Models
{
    public class ToyImage
    {
        public int Id { get; set; }
        public int ToyId { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }

    internal class ImageConfiguration : IEntityTypeConfiguration<ToyImage>
    {
        public void Configure(EntityTypeBuilder<ToyImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Url).HasMaxLength(100);
        }
    }
}
