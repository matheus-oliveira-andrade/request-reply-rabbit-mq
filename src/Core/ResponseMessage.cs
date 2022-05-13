using FluentValidation.Results;

namespace Core;

public class ResponseMessage
{
    public ValidationResult ValidationResult { get; set; }

    public ResponseMessage(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }
}