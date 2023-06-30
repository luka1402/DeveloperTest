
using DeveloperTest.Core.Application.DTO;
using DeveloperTest.Core.Domain;
using DeveloperTest.Core.Domain.EnumList;
using MediatR;

namespace DeveloperTest.Core.Application.Queries;

public class GetAllEmployeesQuery : IRequest<List<EmployeeDto>>
{

}

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = new List<EmployeeDto>();

        var employeesList = await _employeeRepository.GetAllAsync();
        if (employeesList.Any())
        {
            employees.AddRange(employeesList.Select(x=> new EmployeeDto()
            {
                Id = x.Id,
                Email = x.Email,
                Title = Enum.GetName(typeof(TitleTypes), x.Title)
        }));
        }

        return employees;

    }
}

