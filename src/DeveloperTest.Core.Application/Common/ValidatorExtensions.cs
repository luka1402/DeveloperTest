using FluentValidation;
using System.Diagnostics;

namespace DeveloperTest.Core.Application.Common;

public static class ValidatorExtensions
{

    public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Enum errorCode)
    {
        StackTrace stackTrace = new StackTrace();
        var validatorMessage = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name
            .ToUpper()
            .Replace("COMMANDVALIDATOR", "")
            .Replace("QUERYVALIDATOR", "");

        return rule
            .WithErrorCode(errorCode + "__" + validatorMessage.ToUpper());

    }

}
