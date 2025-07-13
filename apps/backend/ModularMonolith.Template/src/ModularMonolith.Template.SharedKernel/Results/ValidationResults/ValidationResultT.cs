using ModularMonolith.Template.SharedKernel.Results.Errors;

namespace ModularMonolith.Template.SharedKernel.Results.ValidationResults
{
    public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
    {
        public ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError)
        {
            Errors = errors;
        }

        public ValidationResult(TValue value, bool isSuccess, Error[] errors) : base(value, isSuccess, IValidationResult.ValidationError)
        {
            Errors = errors;
        }

        public Error[] Errors { get; }
        public static ValidationResult WithErrors(Error[] errors) => new(errors);
    }
}
