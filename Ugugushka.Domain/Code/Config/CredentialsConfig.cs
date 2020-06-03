namespace Ugugushka.Domain.Code.Config
{
    public class CloudinaryCredentials
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string CloudName { get; set; }
    }

    public class AdminAccountCredentials
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CredentialsConfig
    {
        public CloudinaryCredentials Cloudinary { get; set; }
        public AdminAccountCredentials AdminAccount { get; set; }
    }
}
