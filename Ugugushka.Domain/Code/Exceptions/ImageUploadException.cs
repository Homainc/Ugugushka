using System;

namespace Ugugushka.Domain.Code.Exceptions
{
    public class ImageUploadException : Exception
    {
        public string FileName { get; }
        public string Details => $"{Message} (Файл: {FileName})";
        public ImageUploadException(string message, string fileName) : base(message)
        {
            FileName = fileName;
        }
    }
}
