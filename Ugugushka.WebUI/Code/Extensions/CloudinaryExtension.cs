using CloudinaryDotNet;

namespace Ugugushka.WebUI.Code.Extensions
{
    public static class CloudinaryExtension
    {
        public static string BuildImgUrl(this Cloudinary cloudinary, string publicId, string format)
            => cloudinary.Api.UrlImgUp.Secure().Format(format).BuildUrl(publicId);
    }
}
