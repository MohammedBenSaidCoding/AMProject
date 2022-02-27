using AM.Application.AutomowerFeature.Dtos.Output;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AM.Application.AutomowerFeature.Commands.StartOperationCommand;

/// <summary>
/// StartOperationCommand
/// </summary>
[Serializable]
public class StartOperationCommand:IRequest<OperationSummaryDto>
{
    /// <summary>
    /// Operation file
    /// </summary>
    [BindProperty]
    //[Required]
    public IFormFile? File { get; set; }

    /// <summary>
    /// Include mower movement History
    /// </summary>
    [BindProperty]
    public bool IncludeMovementHistory { get; set; }
}