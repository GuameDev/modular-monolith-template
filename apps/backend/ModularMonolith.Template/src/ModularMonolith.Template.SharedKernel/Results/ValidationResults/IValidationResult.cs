using ModularMonolith.Template.SharedKernel.Results.Errors;

namespace ModularMonolith.Template.SharedKernel.Results.ValidationResults
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new Error(
            "ValidationError",
            "A validation problem occurred.",
            ErrorType.Validation);
        Error[] Errors { get; }

    }
}
