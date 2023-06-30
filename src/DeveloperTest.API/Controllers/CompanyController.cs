using DeveloperTest.API._SeedWork;
using DeveloperTest.Core.Application.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
