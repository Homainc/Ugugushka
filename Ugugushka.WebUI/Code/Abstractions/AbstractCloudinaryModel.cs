using CloudinaryDotNet;

namespace Ugugushka.WebUI.Code.Abstractions
{
    public abstract class AbstractCloudinaryModel
    {
        public Cloudinary Cloudinary { get; set; }

        public AbstractCloudinaryModel(Cloudinary cloudinary) =>
            Cloudinary = cloudinary;
    }
}
