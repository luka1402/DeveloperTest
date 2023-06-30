
using DeveloperTest.Core.Application.DTO;
using DeveloperTest.Core.Application.Helper;
using DeveloperTest.Core.Domain;
using DeveloperTest.Core.Domain.EnumList;
using DeveloperTest.Core.Domain.Models;
using MediatR;

namespace DeveloperTest.Core.Application.Commands;

public class CreateCompanyCommand : IRequest<bool>
{
    public string CompanyName { get; set; }
    public List<EmployeeDto>? Employees { get; set; }
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, bool>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeCompanyRepository _employeeCompanyRepository;
    private readonly ISystemLogRepository _systemLogRepository;

    public CreateCompanyCommandHandler(ICompanyRepository companyRepository,
                                    IEmployeeRepository employeeRepository,
                                    IEmployeeCompanyRepository employeeCompanyRepository,
                                    ISystemLogRepository systemLogRepository)
    {
        _companyRepository = companyRepository;
        _employeeRepository = employeeRepository;
        _employeeCompanyRepository = employeeCompanyRepository;
        _systemLogRepository = systemLogRepository;
    }
    public async Task<bool> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        bool isCreated = false;
        Company company = new Company()
        {
            Name = request.CompanyName
        };
        await _companyRepository.AddCompanyAsync(company, cancellationToken);
        var createCompany = await _companyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        if (createCompany > 0 && request.Employees.Any())
        {
            var companyLog = new SystemLog()
            {
                ResourceId = company.Id,
                ResourceType = "Company",
                Event = "Created",
                Changeset = company.ToJson(),
                Comment = $"new company {company.Name} was created",

            };
            await _systemLogRepository.AddSystemLogAsync(companyLog, cancellationToken);
            await _systemLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            companyLog = null;
            foreach (var employee in request.Employees)
            {
                var obj = new Employee()
                {
                    Email = employee.Email,
                    Title = (TitleTypes)Enum.Parse(typeof(TitleTypes), employee.Title)

                };

                await _employeeRepository.AddEmployeeAsync(obj, cancellationToken);
                var createEmployees = await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                if (createEmployees > 0)
                {
                    var objLog = new SystemLog()
                    {
                        ResourceId = obj.Id,
                        ResourceType = "Employee",
                        Event = "Created",
                        Changeset = obj.ToJson(),
                        Comment = $"new employee {obj.Email} was created",

                    };
                    await _systemLogRepository.AddSystemLogAsync(objLog, cancellationToken);
                    await _systemLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                    objLog = null;

                    EmployeeCompany employeeCompany = new EmployeeCompany()
                    {
                        CompanyId = company.Id,
                        EmployeeId = obj.Id
                    };
                    await _employeeCompanyRepository.AddEmployeeCompanyAsync(employeeCompany, cancellationToken);
                    var employeeCompanyCreated = await _employeeCompanyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                    if (employeeCompanyCreated > 0)
                        isCreated = true;
                }
            }

        }
        return isCreated;
    }
}


