using FluentValidation.Results;

namespace DeveloperTest.Core.Application.Common;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
        Codes = new Dictionary<string, string[]>();

    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        string pattern = @"[(\d +)]";

        Errors = failures
            .GroupBy(e =>
                    e.PropertyName.Contains(".") ?
                        e.PropertyName.Split(".").Last() :
                        e.PropertyName,
                e => e.ErrorCode)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

        Codes = failures
            .GroupBy(e => e.PropertyName, e =>
                e.PropertyName.Contains(".") ?
                    e.PropertyName.Split(".").Last().ToUpper() + "_" + e.ErrorCode :
                    e.PropertyName.ToUpper() + "_" + e.ErrorCode)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
    public IDictionary<string, string[]> Codes { get; }

}

