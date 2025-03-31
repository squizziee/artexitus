namespace Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections
{
    public record EmailSettings
    {
        public required string From { get; set; }
        public required string SmtpServer { get; set; }
        public required int Port { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
