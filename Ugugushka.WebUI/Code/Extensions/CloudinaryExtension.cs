using CloudinaryDotNet;

namespace Ugugushka.WebUI.Code.Extensions
{
    public static class CloudinaryExtension
    {
        public static string BuildImgUrl(this Cloudinary cloudinary, string publicId, string format, int? width = null,
            int? height = null)
        {
            var urlBuilder = cloudinary.Api.UrlImgUp.Secure().Format(format);
            
            var transformation = new Transformation();
            if (width.HasValue)
                transformation.Width(width.Value);
            if (height.HasValue)
                transformation.Height(height.Value);
            if (width.HasValue || height.HasValue)
                urlBuilder = urlBuilder.Transform(transformation.Crop("fill"));
            
            return urlBuilder.BuildUrl(publicId);
        }
    }
}
