using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTest.API._SeedWork;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}

