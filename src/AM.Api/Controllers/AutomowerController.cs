using System.Diagnostics.CodeAnalysis;
using AM.Application.AutomowerFeature.Commands.StartOperationCommand;
using AM.Application.AutomowerFeature.Dtos.Output;
using AM.Application.AutomowerFeature.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AM.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AutomowerController : ControllerBase
{
    private readonly IMediator _mediator;
    public AutomowerController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Init lawn and run mowers
    /// </summary>
    /// <param name="command">Start Operation Command</param>
    /// <returns>OperationSummaryDto</returns>
    /// <remarks>
    /// The file is required.
    /// Example of valid file content
    /// 
    /// 5 5
    /// 
    /// 2 3 N
    /// 
    /// FFRFFRLF
    /// 
    /// </remarks>
    /// <response code="200">Returns the operation summary.</response>
    /// <response code="400">If the file is null or the file content is invalid.</response>
    [HttpPost]
    //[ServiceFilter(typeof(ValidateRequestFilter))]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationSummaryDto> StartAsync([FromForm] StartOperationCommand command)
    {
       return await _mediator.Send(command);
    }
}