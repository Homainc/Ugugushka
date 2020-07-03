using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Ugugushka.WebUI.Code.Abstractions
{
    public abstract class AbstractCloudinaryModel
    {
        [ValidateNever]
        public Cloudinary Cloudinary { get; set; }

        public AbstractCloudinaryModel(Cloudinary cloudinary) =>
            Cloudinary = cloudinary;
    }
}
