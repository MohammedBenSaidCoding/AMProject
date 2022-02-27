using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Text.Json;
using AM.Application.AutomowerFeature.Exceptions;
using AM.Domain.LawnAggregate.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MowerAutomatic.Domain.LawnAggregate.Exceptions;

namespace AM.Application.Common.Middlewares;

[ExcludeFromCodeCoverage]
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next??throw new ArgumentNullException(nameof(next));
        _logger = logger??throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
             _logger.LogException(e.Message, e.StackTrace?.ToString());
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        switch (exception)
        {
            case { } and MowerInvalidInstructionException mowerInvalidInstructionException: 
                await HandleMowerInvalidInstructionException(context, mowerInvalidInstructionException);
                break;
            case { } and MowerInvalidStartingPositionException invalidStartingPositionException: 
                await HandleMowerInvalidStartingPositionException(context, invalidStartingPositionException);
                break;
            case { } and InvalidFileContentException invalidFileContentException: 
                await HandleInvalidFileContentException(context, invalidFileContentException);
                break;
            case { } and MowerArgumentException mowerArgumentException: 
               await HandleMowerArgumentException(context, mowerArgumentException);
                break;
            case { } and MowerEmptyInstructionsException mowerEmptyInstructionsException: 
                await HandleMowerEmptyInstructionsException(context, mowerEmptyInstructionsException);
                break;
            case { } and LawnArgumentException lawnArgumentException: 
                await HandleLawnArgumentException(context, lawnArgumentException);
                break;
            case {} and LawInvalidDimensionsException lawInvalidDimensionsException:
                await HandleLawInvalidDimensionsException(context, lawInvalidDimensionsException);
                break;
            case {} and LawnEmptyCommandsException lawnEmptyCommandsException:
                await HandleLawnEmptyCommandsException(context, lawnEmptyCommandsException);
                break;
            case {} and LawnInvalidCommandsCountException lawnInvalidCommandsCountException:
                await HandleLawnInvalidCommandsCountException(context, lawnInvalidCommandsCountException);
                break;
            case { } and InvalidRequestException invalidRequestException:
                await HandleInvalidRequestException(context, invalidRequestException);
                break;
            case {} and ValidationException validationException:
                await HandleValidationException(context, validationException);
                break;
            default: await HandleUndefinedException(context, exception);
                break;

        }
    }
    
    private async Task HandleValidationException(HttpContext context, ValidationException exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        
        var problemDetails = new ProblemDetails
        {
            Title = "Invalid Request Exception",
            Type = nameof(ValidationException),
            Status = context.Response.StatusCode,
            Detail = ConstantsAppMsgException.ValidationFailed,
            Instance = context?.Request?.Path, Extensions = { {ConstantsAppMsgException.Errors , new Dictionary<string, string>() }}
        };
        foreach (var error in exception.Errors)
        {
            var errorsDictionary = (Dictionary<string, string>)problemDetails.Extensions[ConstantsAppMsgException.Errors];
            if (!errorsDictionary.ContainsKey(error.PropertyName))
            {
                errorsDictionary.Add(error.PropertyName, error.ErrorMessage);
            }
        }

        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }
    
    private async Task HandleUndefinedException(HttpContext context, Exception? exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var problemDetails = new ProblemDetails
        {
            Title = ConstantsAppMsgException.InternalServerError,
            Type = ConstantsAppMsgException.UndefinedException,
            Status = context.Response.StatusCode,
            Detail = ConstantsAppMsgException.InternalServerError,
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }

    private async Task HandleInvalidRequestException(HttpContext context, InvalidRequestException exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        var problemDetails = new ProblemDetails
        {
            Title = ConstantsAppMsgException.InvalidRequestException,
            Type = nameof(InvalidRequestException),
            Status = context.Response.StatusCode,
            Detail = exception.Message,
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }


    private async Task HandleMowerInvalidStartingPositionException(HttpContext context, MowerInvalidStartingPositionException exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        var problemDetails = new ProblemDetails
        {
            Title = ConstantsAppMsgException.InvalidFileContentException,
            Type = nameof(HttpStatusCode.BadRequest),
            Status = context.Response.StatusCode,
            Detail = exception.Message,
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }
    
    private async Task HandleMowerInvalidInstructionException(HttpContext context, MowerInvalidInstructionException exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        var problemDetails = new ProblemDetails
        {
            Title = ConstantsAppMsgException.InvalidFileContentException,
            Type = nameof(HttpStatusCode.BadRequest),
            Status = context.Response.StatusCode,
            Detail = exception.Message,
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }
    
    private async Task HandleInvalidFileContentException(HttpContext context, InvalidFileContentException exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        var problemDetails = new ProblemDetails
        {
            Title = ConstantsAppMsgException.InvalidFileContentException,
            Type = nameof(HttpStatusCode.BadRequest),
            Status = context.Response.StatusCode,
            Detail = exception.Message,
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }
    
        
    private async Task HandleMowerEmptyInstructionsException(HttpContext context, MowerEmptyInstructionsException exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        var problemDetails = new ProblemDetails
        {
            Title = "Empty instructions exception.",
            Type = nameof(MowerEmptyInstructionsException),
            Status = context.Response.StatusCode,
            Detail = exception.Message,
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }

    private async Task HandleMowerArgumentException(HttpContext context, MowerArgumentException exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        var problemDetails = new ProblemDetails
        {
            Title =ConstantsAppMsgException.InternalServerError,
            Type = nameof(MowerArgumentException),
            Status = context.Response.StatusCode,
            Detail = "Error occurred during initialization of the mower.",
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }

    private async Task HandleLawnInvalidCommandsCountException(HttpContext context, LawnInvalidCommandsCountException exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        var problemDetails = new ProblemDetails
        {
            Title = "The file content is invalid",
            Type = "FileContentException",
            Status = context.Response.StatusCode,
            Detail = "Error occurred during initialization of the mower. Please check the contents of the file.",
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }

    private async Task HandleLawnArgumentException(HttpContext context, LawnArgumentException exception)
    {
       
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        var problemDetails = new ProblemDetails
        {
            Title = ConstantsAppMsgException.InvalidFileContentException,
            Type = nameof(LawnArgumentException),
            Status = context.Response.StatusCode,
            Detail = "Error occurred during initialization of the lawn.",
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }
    
    private async Task HandleLawInvalidDimensionsException(HttpContext context, LawInvalidDimensionsException exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        var problemDetails = new ProblemDetails
        {
            Title = "Invalid lawn dimensions",
            Type = nameof(LawInvalidDimensionsException),
            Status = context.Response.StatusCode,
            Detail = exception.Message,
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }

    private async Task HandleLawnEmptyCommandsException(HttpContext context, LawnEmptyCommandsException exception)
    {
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        var problemDetails = new ProblemDetails
        {
            Title = "Invalid mower configuration",
            Type = nameof(LawnEmptyCommandsException),
            Status = context.Response.StatusCode,
            Detail = "The file must contain at least the configuration of one mower.",
            Instance = context?.Request?.Path
        };
        if (context != null) await context.Response.Body.WriteAsync(ProblemDetailsAsByte(problemDetails));
    }

    private static byte[] ProblemDetailsAsByte(ProblemDetails problemDetails)
    {
        string problemDetailsAsJson = JsonSerializer.Serialize(problemDetails);
        return Encoding.UTF8.GetBytes(problemDetailsAsJson);
    }
}