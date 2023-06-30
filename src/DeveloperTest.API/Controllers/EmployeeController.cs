using DeveloperTest.API._SeedWork;
using DeveloperTest.Core.Application.Commands;
using DeveloperTest.Core.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees([FromQuery] GetAllEmployeesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
