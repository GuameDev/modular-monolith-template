using System.ComponentModel.DataAnnotations;

namespace ModularMonolith.Template.SharedKernel.Configuration
{
    public sealed class SwaggerOptions : IAppOption
    {
        public static string SectionName => "Swagger";

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Version { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string ContactName { get; set; }

        [Required]
        public required string ContactEmail { get; set; }

        [Required]
        public required string AuthScheme { get; set; }

        [Required]
        public required string AuthHeader { get; set; }

        [Required]
        public required string RoutePrefix { get; set; }
    }
}
