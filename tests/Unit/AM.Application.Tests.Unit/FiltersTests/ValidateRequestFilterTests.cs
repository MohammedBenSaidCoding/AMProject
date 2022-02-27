using System.Collections.Generic;
using AM.Application.AutomowerFeature.Exceptions;
using AM.Application.AutomowerFeature.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace AM.Application.Tests.Unit.FiltersTests;

public class ValidateRequestFilterTests:UnitTestBase
{
    [Fact]
    public void OnActionExecuting_WithoutFile_ThrowInvalidRequestException()
    {
        //Given
        var httpContextMock = new DefaultHttpContext
        {
            Request = {}
        };
        var actionContext = new ActionContext(
            httpContextMock,
            Mock.Of<RouteData>(),
            Mock.Of<ActionDescriptor>(),
            Mock.Of<ModelStateDictionary>()
        );

        var actionExecutingContext = new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            Mock.Of<Controller>()
        );
        
        //when
        void Act()=>new ValidateRequestFilter().OnActionExecuting(actionExecutingContext);
        
        //Then
        AssertThrowException<InvalidRequestException>(Act, ConstantsAppMsgException.StartOperationCommandFileNull);
   
    }
    
    [Theory, AutoMoqData]
    public void OnActionExecuting_WithInvalidFileType_ThrowInvalidRequestException(IFormFile file)
    {
        //Given
        var httpContextMock = new DefaultHttpContext
        {
            Request =
            {
                Form = new FormCollection(new Dictionary<string, StringValues>(),
                    new FormFileCollection() {file})
            }
        };
        var actionContext = new ActionContext(
            httpContextMock,
            Mock.Of<RouteData>(),
            Mock.Of<ActionDescriptor>(),
            Mock.Of<ModelStateDictionary>()
        );

        var actionExecutingContext = new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            Mock.Of<Controller>()
        );

        //when
        void Act()=>new ValidateRequestFilter().OnActionExecuting(actionExecutingContext);
        
        //Then
        AssertThrowException<InvalidRequestException>(Act, ConstantsAppMsgException.InvalidFileContentType);
   
    }
    
    [Fact]
    public void OnActionExecuting_WithNullRequest_ThrowInvalidRequestException()
    {
        //Given
        var actionContext = new ActionContext(
            Mock.Of<HttpContext>(),
            Mock.Of<RouteData>(),
            Mock.Of<ActionDescriptor>(),
            Mock.Of<ModelStateDictionary>()
        );

        var actionExecutingContext = new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            Mock.Of<Controller>()
        );
        
        //when
        void Act()=>new ValidateRequestFilter().OnActionExecuting(actionExecutingContext);
        
        //Then
        AssertThrowException<InvalidRequestException>(Act, ConstantsAppMsgException.StartOperationCommandNull);
   
    }

}