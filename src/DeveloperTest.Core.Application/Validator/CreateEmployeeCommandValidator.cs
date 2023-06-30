using DeveloperTest.Core.Application.Commands;
using DeveloperTest.Core.Application.Common;
using DeveloperTest.Core.Application.Interfaces;
using DeveloperTest.Core.Domain.EnumList;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Core.Application.Validator;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    private readonly IDeveloperTestContext _ctx;

    public CreateEmployeeCommandValidator(IDeveloperTestContext ctx)
    {
        _ctx = ctx;

        RuleFor(x => x.Employee.Email)
            .NotEmpty()
            .WithErrorCode(ErrorCode.IS_EMPTY)
            .EmailAddress()
            .WithErrorCode(ErrorCode.NOT_VALID)
            .Must(EmailMustBeUnique)
            .WithErrorCode(ErrorCode.IS_NOT_UNIQUE);
        RuleFor(x => x.Employee.Title)
            .NotEmpty()
            .WithErrorCode(ErrorCode.IS_EMPTY)
            .Must(EnumValueExists)
            .WithErrorCode(ErrorCode.ENUM_VALUE_NOT_EXIST)
            .Must(TitleExistsInCompany)
            .WithErrorCode(ErrorCode.ALREADY_EXIST);
        RuleFor(x => x.Employee.CompanyId)
            .NotEmpty()
            .WithErrorCode(ErrorCode.IS_EMPTY)
            .NotNull()
            .WithErrorCode(ErrorCode.IS_NULL)
            .Must(CompanyExists)
            .WithErrorCode(ErrorCode.NOT_EXIST);
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

    private bool CompanyExists(int? companyId)
    {
        return _ctx.Companies.Any(x => x.Id == companyId);

    }

    private bool TitleExistsInCompany(CreateEmployeeCommand instance, string title)
    {
        bool titleExists = true;
        var enumValueExists = EnumValueExists(title);
        if (enumValueExists)
        {
            var result = _ctx.EmployeeCompanies
                .Include(x => x.Employee)
                .Include(x => x.Company)
                .FirstOrDefault(x => x.CompanyId == instance.Employee.CompanyId &&
                                     x.Employee.Title == (TitleTypes)Enum.Parse(typeof(TitleTypes), instance.Employee.Title)
                                     );
            if (result is not null)
                titleExists = false;
        }



        return titleExists;
    }
}

