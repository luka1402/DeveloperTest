using DeveloperTest.Core.Application.Commands;
using DeveloperTest.Core.Application.Common;
using DeveloperTest.Core.Application.DTO;
using DeveloperTest.Core.Application.Interfaces;
using DeveloperTest.Core.Domain.EnumList;
using FluentValidation;

namespace DeveloperTest.Core.Application.Validator;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    private readonly IDeveloperTestContext _ctx;

    public CreateCompanyCommandValidator(IDeveloperTestContext ctx)
    {
        _ctx = ctx;

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithErrorCode(ErrorCode.IS_EMPTY)
            .Must(CompanyNameExists)
            .WithErrorCode(ErrorCode.ALREADY_EXIST);
        RuleFor(x => x.Employees)
            .Must(EmployeeListEmailUnique)
            .WithErrorCode(CustomErrorCode.EMPLOYEES_LIST_EMAIL_IS_NOT_UNIQUE)
            .Must(EmployeeListTitleUnique)
            .WithErrorCode(CustomErrorCode.EMPLOYEES_LIST_TITLE_IS_NOT_UNIQUE);

        RuleForEach(x => x.Employees).ChildRules(employee =>
        {
            employee.RuleFor(x => x.Email)
                .NotEmpty()
                .WithErrorCode(ErrorCode.IS_EMPTY)
                .EmailAddress()
                .WithErrorCode(ErrorCode.NOT_VALID)
                .Must(EmailMustBeUnique)
                .WithErrorCode(ErrorCode.IS_NOT_UNIQUE);
            employee.RuleFor(x => x.Title)
                .NotEmpty()
                .WithErrorCode(ErrorCode.IS_EMPTY)
                .Must(EnumValueExists)
                .WithErrorCode(ErrorCode.ENUM_VALUE_NOT_EXIST);


        });
    }
    private bool EmployeeListTitleUnique(List<EmployeeDto>? employees)
    {
        var titleSet = new HashSet<string>();

        foreach (var employee in employees)
        {
            if (string.IsNullOrEmpty(employee.Title))
            {
                continue;
            }

            if (!titleSet.Add(employee.Title))
            {
                return false;
            }
        }

        return true;
    }
    private bool EmployeeListEmailUnique(List<EmployeeDto>? employees)
    {
        var emailSet = new HashSet<string>();

        foreach (var employee in employees)
        {
            if (string.IsNullOrEmpty(employee.Email))
            {
                continue;
            }

            if (!emailSet.Add(employee.Email))
            {
                return false;
            }
        }

        return true;
    }
    private bool CompanyNameExists(string companyName)
    {
        return !_ctx.Companies.Any(x => x.Name == companyName);
    }
    private bool EmailMustBeUnique(string email)
    {
        return !_ctx.Employees.Any(x => x.Email == email);
    }

    private bool EnumValueExists(string title)
    {
        if (Enum.TryParse<TitleTypes>(title, out var result))
        {
            return Enum.IsDefined(typeof(TitleTypes), result);
        }
        return false;
    }

}

