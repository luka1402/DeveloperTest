using DeveloperTest.Core.Application.DTO;
using DeveloperTest.Core.Application.Helper;
using DeveloperTest.Core.Domain;
using DeveloperTest.Core.Domain.EnumList;
using DeveloperTest.Core.Domain.Models;
using MediatR;

namespace DeveloperTest.Core.Application.Commands;

public class CreateEmployeeCommand : IRequest<bool>
{
    public EmployeeDto Employee { get; set; }

}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, bool>
{

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeCompanyRepository _employeeCompanyRepository;
    private readonly ISystemLogRepository _systemLogRepository;


    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
                                        IEmployeeCompanyRepository employeeCompanyRepository,
                                        ISystemLogRepository systemLogRepository)
    {

        _employeeRepository = employeeRepository;
        _employeeCompanyRepository = employeeCompanyRepository;
        _systemLogRepository = systemLogRepository;
    }
    public async Task<bool> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        bool isCreated = false;

        Employee employee = new Employee()
        {
            Email = request.Employee.Email,
            Title = (TitleTypes)Enum.Parse(typeof(TitleTypes), request.Employee.Title)
        };

        await _employeeRepository.AddEmployeeAsync(employee, cancellationToken);
        var insertEmployee = await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        if (insertEmployee > 0)
        {

            var objLog = new SystemLog()
            {
                ResourceId = employee.Id,
                ResourceType = "Employee",
                Event = "Created",
                Changeset = employee.ToJson(),
                Comment = $"new employee {employee.Email} was created",

            };
            await _systemLogRepository.AddSystemLogAsync(objLog, cancellationToken);
            await _systemLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            objLog = null;

            EmployeeCompany employeeCompany = new EmployeeCompany()
            {
                CompanyId = request.Employee.CompanyId.Value,
                EmployeeId = employee.Id
            };


            await _employeeCompanyRepository.AddEmployeeCompanyAsync(employeeCompany, cancellationToken);
            var insertEmployeeCompany = await _employeeCompanyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (insertEmployeeCompany > 0)
                isCreated = true;
        }



        return isCreated;
    }
}

