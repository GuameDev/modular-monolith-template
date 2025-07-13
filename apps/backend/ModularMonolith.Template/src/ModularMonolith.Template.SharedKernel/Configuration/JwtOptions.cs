using System.ComponentModel.DataAnnotations;

namespace ModularMonolith.Template.SharedKernel.Configuration
{
    public class JwtOptions : IAppOption
    {
        public static string SectionName => "Jwt";

        [Required]
        public required AccessToken AccessToken { get; set; }
        [Required]
        public required RefreshToken RefreshToken { get; set; }
    }

    public class AccessToken
    {
        [Required]
        public required string Key { get; set; }
        [Required]
        public required string Issuer { get; set; }
        [Required]
        public required string Audience { get; set; }
        [Required]
        public int ExpirationDateInMinutes { get; set; }
    }
    public class RefreshToken
    {
        [Required]
        public int ExpirationDateInDays { get; set; }
    }
}
