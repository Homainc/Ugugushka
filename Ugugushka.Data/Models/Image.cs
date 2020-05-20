using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ugugushka.Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int ToyId { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }

    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Url).HasMaxLength(100);
        }
    }
}
